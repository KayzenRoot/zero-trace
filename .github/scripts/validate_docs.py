"""Documentation-only validation. No Unity/game execution."""
from collections import Counter
import hashlib
import json
import os
from pathlib import Path
import re
import subprocess
import xml.etree.ElementTree as ET

def require(ok, message):
    if not ok:
        raise SystemExit("DOCS_VALIDATION_FAIL: " + message)

def read(path):
    return Path(path).read_text(encoding="utf-8")

def rows(text, pattern):
    return [[c.strip() for c in l.strip("|").split("|")] for l in text.splitlines() if re.match(pattern, l)]

gdd_path = "design/ZERO_TRACE_GDD_MASTER_v0.2.0.md"
catalog_path = "design/ZERO_TRACE_Mission_Catalog_v0.2.0.json"
for path, expected in {
    gdd_path:"4352e4a2bdf4f7d3925caaa6314ba1b550efe1c3f917ba05acd50aa940ea5ed1",
    catalog_path:"836e305e1fb9777ba1298e76d93b8bfa97d77ebc10b7ed846fae3477e1b0b365",
}.items():
    require(hashlib.sha256(Path(path).read_bytes()).hexdigest()==expected, path)
gdd=read(gdd_path)
m=json.loads(read(catalog_path))["missions"]
by_id={x["id"]:x for x in m}
require(len(m)==len(by_id)==420, "mission count/IDs")
require(len({x["localization_key"] for x in m})==420,"localization IDs")
require(Counter(x["category"] for x in m)=={"CAM":60,"PRO":100,"INV":50,"CLN":40,"ACA":100,"COO":40,"EVT":30},"mission categories")
versions=["V0.1","V0.2","V0.5","V1.0"]
gates=dict(zip(versions,["D09","D10","D16","D18"]))
categories=["CAM","PRO","INV","CLN","ACA","COO","EVT"]
cumulative=[[2,2,1,0,5,0,0],[5,5,3,2,10,0,0],[25,45,20,15,45,12,8],[60,100,50,40,100,40,30]]
for i,v in enumerate(versions):
    observed=[sum(x["category"]==c and versions.index(x["release"])<=i for x in m) for c in categories]
    require(observed==cumulative[i],"cumulative category matrix "+v)
require(all(x["release_gate"]==gates[x["release"]] and x["status"]=="BRIEF" for x in m),"immutable original brief release gates")
refs={}
for x in m:
    pre=x["prerequisite"]
    if re.fullmatch(r"[A-Z]{3}-\d{3}",pre):
        require(pre in by_id and versions.index(by_id[pre]["release"])<=versions.index(x["release"]),"mission prerequisite "+x["id"])
        refs[x["id"]]=pre
    else:
        require(pre in {"onboarding","competência da trilha e autorização virtual","regra do evento e pareamento"},"unknown prerequisite policy "+x["id"])
for start in refs:
    seen=set()
    current=start
    while current in refs:
        require(current not in seen,"mission prerequisite cycle "+start)
        seen.add(current)
        current=refs[current]
require(len(refs)==356,"sequential prerequisites")
required="README Checkpoint Decisions Scope DefinitionOfDone Architecture Requirements Security TestPlan Deployment Backlog Sources ProjectOverview ModelRouting DataAndContracts VisualSpec VisualManifest ContextLock Governance Preflight".split()
for name in required:
    require(Path("docs",name+".md").is_file(),"missing "+name)
require(Path("AGENTS.md").is_file(),"missing AGENTS")
graph={}
for row in rows(read("docs/Backlog.md"),r"^\| ZT-"):
    wo=row[0]
    node="D00" if wo=="ZT-M00-WO-001" else wo.split("-")[1]
    require(node not in graph,"duplicate "+node)
    graph[node]=set(re.findall(r"D\d{2}",row[3]))
    require(Path("docs/work-orders",wo+".md").is_file(),"missing WO "+wo)
canonical={r[0]:set(re.findall(r"D\d{2}",r[1])) for r in rows(gdd,r"^\| D\d{2} \|")}
require(graph==canonical and len(graph)==20,"normative D00-D19 graph mismatch")
visited=set()
while len(visited)<20:
    ready={k for k,v in graph.items() if k not in visited and v<=visited}
    require(bool(ready),"dependency cycle/unknown node")
    visited|=ready
canonical_apps={r[0][:3]:r[0][4:] for r in rows(gdd,r"^\| A\d{2} ")}
app_rows=rows(read("docs/VisualSpec.md"),r"^\| A\d{2} \|")
require(len(app_rows)==30 and {r[0]:r[1] for r in app_rows}==canonical_apps,"30 canonical application IDs/names")
expected_apps={
    "V0.1":"A01 A02 A04 A10 A16 A18 A26 A27 A28",
    "V0.2":"A06 A08 A13 A14",
    "V0.5":"A03 A05 A07 A09 A11 A12 A15 A17 A19 A20 A23 A25 A30",
    "V1.0":"A21 A22 A24 A29"
}
require(all(set(r[0] for r in app_rows if r[3]==v)==set(ids.split()) for v,ids in expected_apps.items()),"app release matrix")
ns="{http://www.w3.org/2000/svg}"
for name,view in {"desktop-en-us.svg":"0 0 1920 1080","desktop-pt-br-150.svg":"0 0 1280 720","priority-and-states.svg":"0 0 1920 1080"}.items():
    path="design/wireframes/"+name
    root=ET.parse(path).getroot()
    require(root.tag==ns+"svg" and root.get("width")=="1920" and root.get("height")=="1080" and root.get("viewBox")==view,"SVG dimensions "+name)
    require(all(e.tag in {ns+t for t in ["svg","title","defs","linearGradient","stop","g","rect","text"]} and not any(k.lower().startswith("on") or k.endswith("href") for k in e.attrib) for e in root.iter()),"SVG executable/external elements "+name)
    labels=[e.text for e in root.iter(ns+"text")]
    if name=="desktop-en-us.svg":
        require(all(labels.count(id)==1 and nm in labels for id,nm in canonical_apps.items()),"launcher labels")
    elif name=="priority-and-states.svg":
        require(all("["+s+"]" in labels for s in ["EMPTY","PENDING","FAILURE","DENIED","RECOVERY"]),"critical state coverage")
    elif name=="desktop-pt-br-150.svg":
        require("PT-BR · UI 150%" in labels and "Retomar com segurança" in labels,"scaled locale reference")
    blob=subprocess.check_output(["git","hash-object","--",path],text=True).strip()
    require(blob in read("docs/VisualManifest.md"),"SVG manifest mismatch "+name)
rules=json.loads(read(".github/governance/main-ruleset.proposed.json"))
require(rules["enforcement"]=="active" and rules["bypass_actors"]==[] and rules["conditions"]["ref_name"]["include"]==["refs/heads/main"],"proposed governance target")
by_rule={r["type"]:r.get("parameters",{}) for r in rules["rules"]}
require({"deletion","non_fast_forward","required_linear_history","pull_request","required_status_checks"}<=set(by_rule),"proposed governance rules")
require(by_rule["pull_request"]["allowed_merge_methods"]==["squash"] and by_rule["required_status_checks"]["strict_required_status_checks_policy"] and {"context":"docs-validation"} in by_rule["required_status_checks"]["required_status_checks"],"proposed governance checks")
# This validates the versioned proposal only, not live settings.
lock=json.loads(read("docs/locks/ZT-M00-WO-001.json"))
if "State: D00 BLOCKED" in read("docs/Checkpoint.md"):
    require(lock["status"]=="CANDIDATE_BLOCKED","D00 lock state")
    require(lock["base_main_sha"]=="932c5b83b7b160c38138e2c1ed506102a0ffe178","D00 baseline")
    for path,expected in lock["git_blobs"].items():
        actual=subprocess.check_output(["git","hash-object","--",path],text=True).strip()
        require(actual==expected,"STALE D00 lock "+path)
# The historical candidate lock stays immutable. Validate promotion fingerprints on the
# D00 PR, the squash integration push, and local D00 branch checkouts.
if "State: D00 APPROVED_PENDING_INTEGRATION" in read("docs/Checkpoint.md"):
    promotion=json.loads(read("docs/locks/ZT-M00-WO-001-promotion.json"))
    require(promotion["status"]=="APPROVED_PENDING_INTEGRATION" and promotion["base_main_sha"]==lock["base_main_sha"],"promotion lock status/base")
    require(promotion["audited_candidate_sha"]=="b337e5ae754052623a900d27a9fb6516d1a98d2e","audited candidate")
    require(set(lock["git_blobs"]) <= set(promotion["git_blobs"]),"promotion lock coverage")
    event={}
    if os.environ.get("GITHUB_EVENT_PATH"):
        event=json.loads(Path(os.environ["GITHUB_EVENT_PATH"]).read_text(encoding="utf-8"))
    branch=subprocess.check_output(["git","branch","--show-current"],text=True).strip()
    message=subprocess.check_output(["git","log","-1","--format=%s"],text=True).strip()
    d00_context=(event.get("number")==1 or branch=="docs/zt-m00-wo-001" or message.startswith("docs(ZT-M00-WO-001):"))
    if d00_context:
        for path,expected in promotion["git_blobs"].items():
            actual=subprocess.check_output(["git","hash-object","--",path],text=True).strip()
            require(actual==expected,"STALE promotion lock "+path)
        print("D00_PROMOTION_LOCK_OK: "+str(len(promotion["git_blobs"]))+" fingerprints")
    else:
        print("D00 historical lock; subsequent WOs require their own fresh execution lock")
print("DOCS_VALIDATION_OK: 420 original briefs; 356 sequential prerequisites; release matrix; 20-node canonical DAG; 30 apps; 3 static SVGs; proposed governance; lock checks")
