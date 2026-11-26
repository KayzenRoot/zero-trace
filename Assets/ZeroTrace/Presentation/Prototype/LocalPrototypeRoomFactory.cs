using UnityEngine;

namespace ZeroTrace.Presentation
{
    public sealed class LocalPrototypeRoom
    {
        internal GameObject Root;
        internal GameObject Player;
        internal Collider ComputerCollider;
        internal Camera Camera;
        internal LocalPrototypePlayerMotor Motor;
        internal Transform OriginalCameraParent;
        internal Vector3 OriginalCameraLocalPosition;
        internal Quaternion OriginalCameraLocalRotation;
        internal Vector3 OriginalCameraLocalScale;
        internal bool OriginalCameraEnabled;
    }

    public static class LocalPrototypeRoomFactory
    {
        private static readonly Color Obsidian = new Color32(11, 13, 16, 255);
        private static readonly Color Carbon = new Color32(26, 30, 36, 255);
        private static readonly Color Frost = new Color32(230, 237, 246, 255);
        private static readonly Color Lime = new Color32(183, 255, 46, 255);
        private static readonly Color Cyan = new Color32(93, 235, 255, 255);

        public static LocalPrototypeRoom Create(Camera camera, out string error)
        {
            error = null;
            if (camera == null)
            {
                error = "The existing scene camera could not be found.";
                return null;
            }

            var room = new LocalPrototypeRoom
            {
                Camera = camera,
                OriginalCameraParent = camera.transform.parent,
                OriginalCameraLocalPosition = camera.transform.localPosition,
                OriginalCameraLocalRotation = camera.transform.localRotation,
                OriginalCameraLocalScale = camera.transform.localScale,
                OriginalCameraEnabled = camera.enabled,
                Root = new GameObject("ZERO TRACE Local Prototype Room")
            };

            CreateCube(room.Root.transform, "Room Floor", new Vector3(0f, -0.12f, 0.25f),
                new Vector3(8f, 0.24f, 8f), Carbon, true);
            CreateCube(room.Root.transform, "Back Wall", new Vector3(0f, 1.5f, 4.15f),
                new Vector3(8f, 3f, 0.25f), Obsidian, true);
            CreateCube(room.Root.transform, "Front Wall (Negative Z)", new Vector3(0f, 1.5f, -3.9f),
                new Vector3(8f, 3f, 0.25f), Obsidian, true);
            CreateCube(room.Root.transform, "Left Wall", new Vector3(-4.05f, 1.5f, 0.2f),
                new Vector3(0.25f, 3f, 8f), Carbon, true);
            CreateCube(room.Root.transform, "Right Wall", new Vector3(4.05f, 1.5f, 0.2f),
                new Vector3(0.25f, 3f, 8f), Carbon, true);
            CreateCube(room.Root.transform, "Ceiling", new Vector3(0f, 3.05f, 0.2f),
                new Vector3(8f, 0.15f, 8f), Obsidian, true);
            CreateCube(room.Root.transform, "Lime Wall Inlay", new Vector3(-3.88f, 1.6f, 1.5f),
                new Vector3(0.025f, 1.8f, 2.2f), Lime, false);
            CreateCube(room.Root.transform, "Cyan Wall Inlay", new Vector3(3.88f, 1.6f, 1.2f),
                new Vector3(0.025f, 1.8f, 2.8f), Cyan, false);

            CreateDesk(room.Root.transform);
            var computer = CreateComputer(room.Root.transform);
            CreateShelf(room.Root.transform);
            CreatePointLight(room.Root.transform);

            var player = new GameObject("Local Prototype Player");
            player.transform.SetParent(room.Root.transform, false);
            player.transform.localPosition = new Vector3(0f, 0f, -2.4f);
            var characterController = player.AddComponent<CharacterController>();
            characterController.height = 1.8f;
            characterController.radius = 0.28f;
            characterController.center = new Vector3(0f, 0.92f, 0f);
            characterController.stepOffset = 0.25f;
            characterController.slopeLimit = 45f;

            camera.transform.SetParent(player.transform, false);
            camera.transform.localPosition = new Vector3(0f, 1.58f, 0f);
            camera.transform.localRotation = Quaternion.identity;
            camera.enabled = true;
            room.Player = player;
            room.ComputerCollider = computer;
            room.Motor = player.AddComponent<LocalPrototypePlayerMotor>();
            var playableBounds = new Bounds(new Vector3(0f, 0.7f, 0.25f), new Vector3(7.2f, 2.4f, 6.8f));
            room.Motor.Configure(characterController, camera.transform, playableBounds, player.transform.position);

            Physics.SyncTransforms();
            return room;
        }

        public static void Restore(LocalPrototypeRoom room)
        {
            if (room == null)
            {
                return;
            }

            if (room.Camera != null)
            {
                room.Camera.transform.SetParent(room.OriginalCameraParent, false);
                room.Camera.transform.localPosition = room.OriginalCameraLocalPosition;
                room.Camera.transform.localRotation = room.OriginalCameraLocalRotation;
                room.Camera.transform.localScale = room.OriginalCameraLocalScale;
                room.Camera.enabled = room.OriginalCameraEnabled;
            }

            if (room.Root != null)
            {
                var renderers = room.Root.GetComponentsInChildren<Renderer>(true);
                foreach (var renderer in renderers)
                {
                    if (renderer != null && renderer.sharedMaterial != null)
                    {
                        Object.Destroy(renderer.sharedMaterial);
                    }
                }
            }

            if (room.Motor != null)
            {
                room.Motor.SetControlsEnabled(false);
            }

            if (room.Root != null)
            {
                Object.Destroy(room.Root);
            }
        }

        private static void CreateDesk(Transform parent)
        {
            CreateCube(parent, "Desk Top", new Vector3(0f, 0.82f, 0.2f), new Vector3(2.4f, 0.12f, 1f),
                new Color32(50, 57, 66, 255), true);
            CreateCube(parent, "Desk Leg Left", new Vector3(-1.04f, 0.4f, -0.15f), new Vector3(0.12f, 0.75f, 0.12f), Carbon, true);
            CreateCube(parent, "Desk Leg Right", new Vector3(1.04f, 0.4f, -0.15f), new Vector3(0.12f, 0.75f, 0.12f), Carbon, true);
            CreateCube(parent, "Desk Rear Support", new Vector3(0f, 0.42f, 0.52f), new Vector3(2.15f, 0.1f, 0.1f), Carbon, true);
            CreateCube(parent, "Keyboard", new Vector3(0f, 0.91f, -0.05f), new Vector3(0.8f, 0.045f, 0.25f), Obsidian, false);
            CreateCube(parent, "Keyboard Accent", new Vector3(0f, 0.936f, -0.05f), new Vector3(0.52f, 0.012f, 0.014f), Lime, false);
        }

        private static Collider CreateComputer(Transform parent)
        {
            CreateCube(parent, "Monitor Stand", new Vector3(0f, 1.06f, 0.43f), new Vector3(0.16f, 0.38f, 0.12f),
                new Color32(74, 83, 95, 255), false);
            CreateCube(parent, "Monitor Base", new Vector3(0f, 0.92f, 0.42f), new Vector3(0.48f, 0.035f, 0.22f),
                new Color32(74, 83, 95, 255), false);
            var monitor = CreateCube(parent, "Training Computer", new Vector3(0f, 1.43f, 0.46f),
                new Vector3(1.25f, 0.76f, 0.16f), new Color32(36, 43, 51, 255), true);
            CreateCube(parent, "Monitor Screen", new Vector3(0f, 1.43f, 0.372f),
                new Vector3(1.12f, 0.63f, 0.012f), new Color32(14, 34, 42, 255), false);
            CreateCube(parent, "Monitor Status Line", new Vector3(-0.35f, 1.18f, 0.363f),
                new Vector3(0.45f, 0.018f, 0.01f), Cyan, false);
            return monitor.GetComponent<Collider>();
        }

        private static void CreateShelf(Transform parent)
        {
            CreateCube(parent, "Wall Shelf", new Vector3(2.7f, 1.35f, 2.4f), new Vector3(1.5f, 0.08f, 0.55f),
                new Color32(44, 51, 59, 255), true);
            CreateCube(parent, "Fictional Storage Case", new Vector3(2.6f, 1.65f, 2.45f), new Vector3(0.45f, 0.52f, 0.36f),
                new Color32(38, 62, 67, 255), true);
            CreateCube(parent, "Shelf Label", new Vector3(2.6f, 1.67f, 2.26f), new Vector3(0.28f, 0.06f, 0.016f),
                Cyan, false);
        }

        private static void CreatePointLight(Transform parent)
        {
            var lightObject = new GameObject("Room Fill Light");
            lightObject.transform.SetParent(parent, false);
            lightObject.transform.localPosition = new Vector3(0f, 2.62f, 0.3f);
            var light = lightObject.AddComponent<Light>();
            light.type = LightType.Point;
            light.color = new Color32(168, 204, 218, 255);
            light.intensity = 2.4f;
            light.range = 8f;
        }

        private static GameObject CreateCube(Transform parent, string name, Vector3 position, Vector3 scale, Color color, bool collider)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = name;
            cube.transform.SetParent(parent, false);
            cube.transform.localPosition = position;
            cube.transform.localScale = scale;
            if (!collider)
            {
                var box = cube.GetComponent<Collider>();
                if (box != null)
                {
                    box.enabled = false;
                }
            }

            var renderer = cube.GetComponent<Renderer>();
            var shader = Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard");
            if (renderer != null && shader != null)
            {
                var material = new Material(shader) { color = color };
                if (color == Lime || color == Cyan)
                {
                    material.EnableKeyword("_EMISSION");
                    material.SetColor("_EmissionColor", color * 0.18f);
                }

                renderer.sharedMaterial = material;
            }

            return cube;
        }
    }
}
