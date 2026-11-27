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

            CreateBaseTrim(room.Root.transform);
            CreateEntrance(room.Root.transform);
            CreateWindow(room.Root.transform);
            CreateLivingArea(room.Root.transform);

            CreateDesk(room.Root.transform);
            var computer = CreateComputer(room.Root.transform);
            CreateStorage(room.Root.transform);
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
            CreateCube(parent, "Keyboard Accent", new Vector3(0f, 0.936f, -0.05f), new Vector3(0.2f, 0.012f, 0.014f), Lime, false);
        }

        private static Collider CreateComputer(Transform parent)
        {
            CreateCube(parent, "Monitor Stand", new Vector3(0f, 1.06f, 0.43f), new Vector3(0.16f, 0.38f, 0.12f),
                new Color32(74, 83, 95, 255), false);
            CreateCube(parent, "Monitor Base", new Vector3(0f, 0.92f, 0.42f), new Vector3(0.48f, 0.035f, 0.22f),
                new Color32(74, 83, 95, 255), false);
            var monitor = CreateCube(parent, "Recovery Computer Monitor", new Vector3(0f, 1.43f, 0.46f),
                new Vector3(1.25f, 0.76f, 0.16f), new Color32(36, 43, 51, 255), true);
            CreateCube(parent, "Monitor Screen", new Vector3(0f, 1.43f, 0.372f),
                new Vector3(1.12f, 0.63f, 0.012f), new Color32(14, 34, 42, 255), false);
            CreateCube(parent, "Monitor Status Line", new Vector3(-0.35f, 1.18f, 0.363f),
                new Vector3(0.2f, 0.012f, 0.01f), Cyan, false);
            return monitor.GetComponent<Collider>();
        }

        private static void CreateBaseTrim(Transform parent)
        {
            var trim = new Color32(41, 47, 55, 255);
            CreateCube(parent, "Back Wall Base Trim", new Vector3(0f, 0.08f, 3.985f),
                new Vector3(7.8f, 0.16f, 0.06f), trim, false);
            CreateCube(parent, "Front Wall Base Trim", new Vector3(0f, 0.08f, -3.74f),
                new Vector3(7.8f, 0.16f, 0.06f), trim, false);
            CreateCube(parent, "Left Wall Base Trim", new Vector3(-3.89f, 0.08f, 0.2f),
                new Vector3(0.06f, 0.16f, 7.8f), trim, false);
            CreateCube(parent, "Right Wall Base Trim", new Vector3(3.89f, 0.08f, 0.2f),
                new Vector3(0.06f, 0.16f, 7.8f), trim, false);
        }

        private static void CreateEntrance(Transform parent)
        {
            var doorX = -2.55f;
            var doorZ = -3.715f;
            var frame = new Color32(66, 74, 83, 255);
            CreateCube(parent, "Apartment Entrance Door Panel", new Vector3(doorX, 1.04f, doorZ),
                new Vector3(1.02f, 2.02f, 0.035f), new Color32(35, 41, 49, 255), false);
            CreateCube(parent, "Apartment Entrance Door Inset", new Vector3(doorX, 1.06f, doorZ + 0.022f),
                new Vector3(0.78f, 1.7f, 0.012f), new Color32(45, 52, 61, 255), false);
            CreateCube(parent, "Apartment Entrance Left Jamb", new Vector3(doorX - 0.59f, 1.08f, doorZ + 0.015f),
                new Vector3(0.1f, 2.16f, 0.08f), frame, false);
            CreateCube(parent, "Apartment Entrance Right Jamb", new Vector3(doorX + 0.59f, 1.08f, doorZ + 0.015f),
                new Vector3(0.1f, 2.16f, 0.08f), frame, false);
            CreateCube(parent, "Apartment Entrance Lintel", new Vector3(doorX, 2.16f, doorZ + 0.015f),
                new Vector3(1.28f, 0.1f, 0.08f), frame, false);
            CreateCube(parent, "Apartment Entrance Handle", new Vector3(doorX + 0.38f, 1.02f, doorZ + 0.04f),
                new Vector3(0.035f, 0.12f, 0.025f), Frost, false);
        }

        private static void CreateWindow(Transform parent)
        {
            var windowX = 3.895f;
            var windowY = 1.95f;
            var windowZ = 2.0f;
            var frame = new Color32(53, 61, 70, 255);
            CreateCube(parent, "Apartment Window Pane", new Vector3(windowX, windowY, windowZ),
                new Vector3(0.024f, 1.02f, 1.82f), new Color32(77, 108, 123, 255), false);
            CreateCube(parent, "Apartment Window Left Frame", new Vector3(windowX - 0.02f, windowY, windowZ - 0.98f),
                new Vector3(0.08f, 1.2f, 0.08f), frame, false);
            CreateCube(parent, "Apartment Window Right Frame", new Vector3(windowX - 0.02f, windowY, windowZ + 0.98f),
                new Vector3(0.08f, 1.2f, 0.08f), frame, false);
            CreateCube(parent, "Apartment Window Top Frame", new Vector3(windowX - 0.02f, windowY + 0.59f, windowZ),
                new Vector3(0.08f, 0.08f, 2.04f), frame, false);
            CreateCube(parent, "Apartment Window Sill", new Vector3(windowX - 0.02f, windowY - 0.59f, windowZ),
                new Vector3(0.14f, 0.1f, 2.04f), frame, true);
            CreateCube(parent, "Apartment Window Center Mullion", new Vector3(windowX - 0.025f, windowY, windowZ),
                new Vector3(0.06f, 1.02f, 0.045f), frame, false);
            CreateCube(parent, "Apartment Window Exterior Horizon", new Vector3(windowX - 0.01f, 1.73f, windowZ),
                new Vector3(0.012f, 0.025f, 1.62f), new Color32(94, 137, 150, 255), false);
        }

        private static void CreateLivingArea(Transform parent)
        {
            var sofaBase = new Color32(73, 86, 98, 255);
            var cushion = new Color32(104, 119, 132, 255);
            CreateCube(parent, "Apartment Living Rug", new Vector3(-2.62f, 0.012f, 0.82f),
                new Vector3(1.75f, 0.024f, 2.2f), new Color32(31, 42, 50, 255), false);
            CreateCube(parent, "Apartment Sofa Seat", new Vector3(-3.24f, 0.38f, 0.82f),
                new Vector3(0.88f, 0.34f, 1.65f), sofaBase, true);
            CreateCube(parent, "Apartment Sofa Cushion", new Vector3(-3.2f, 0.57f, 0.82f),
                new Vector3(0.72f, 0.12f, 1.46f), cushion, false);
            CreateCube(parent, "Apartment Sofa Back", new Vector3(-3.68f, 0.75f, 0.82f),
                new Vector3(0.16f, 0.72f, 1.68f), sofaBase, true);
            CreateCube(parent, "Apartment Sofa Front Arm", new Vector3(-3.2f, 0.59f, -0.02f),
                new Vector3(0.9f, 0.46f, 0.16f), sofaBase, true);
            CreateCube(parent, "Apartment Sofa Rear Arm", new Vector3(-3.2f, 0.59f, 1.66f),
                new Vector3(0.9f, 0.46f, 0.16f), sofaBase, true);
            CreateCube(parent, "Apartment Sofa Front Left Foot", new Vector3(-3.55f, 0.16f, 0.18f),
                new Vector3(0.08f, 0.28f, 0.08f), Carbon, true);
            CreateCube(parent, "Apartment Sofa Rear Left Foot", new Vector3(-3.55f, 0.16f, 1.46f),
                new Vector3(0.08f, 0.28f, 0.08f), Carbon, true);
            CreateCube(parent, "Apartment Sofa Front Right Foot", new Vector3(-2.94f, 0.16f, 0.18f),
                new Vector3(0.08f, 0.28f, 0.08f), Carbon, true);
            CreateCube(parent, "Apartment Sofa Rear Right Foot", new Vector3(-2.94f, 0.16f, 1.46f),
                new Vector3(0.08f, 0.28f, 0.08f), Carbon, true);

            CreateCube(parent, "Apartment Living Lamp Base", new Vector3(-3.05f, 0.08f, -0.3f),
                new Vector3(0.28f, 0.12f, 0.28f), Carbon, false);
            CreateCube(parent, "Apartment Living Lamp Stem", new Vector3(-3.05f, 0.88f, -0.3f),
                new Vector3(0.045f, 1.5f, 0.045f), FrameColor(), false);
            CreateCube(parent, "Apartment Living Lamp Shade", new Vector3(-3.05f, 1.68f, -0.3f),
                new Vector3(0.3f, 0.22f, 0.3f), new Color32(177, 170, 145, 255), false);

            var lampObject = new GameObject("Apartment Living Lamp Light");
            lampObject.transform.SetParent(parent, false);
            lampObject.transform.localPosition = new Vector3(-3.05f, 1.58f, -0.3f);
            var lamp = lampObject.AddComponent<Light>();
            lamp.type = LightType.Point;
            lamp.color = new Color32(255, 214, 158, 255);
            lamp.intensity = 0.45f;
            lamp.range = 2.8f;
            lamp.shadows = LightShadows.None;
        }

        private static void CreatePointLight(Transform parent)
        {
            var lightObject = new GameObject("Room Fill Light");
            lightObject.transform.SetParent(parent, false);
            lightObject.transform.localPosition = new Vector3(0f, 2.62f, 0.3f);
            var light = lightObject.AddComponent<Light>();
            light.type = LightType.Point;
            light.color = new Color32(185, 207, 220, 255);
            light.intensity = 1.6f;
            light.range = 8f;
            light.shadows = LightShadows.None;
        }

        private static Color FrameColor()
        {
            return new Color32(76, 83, 91, 255);
        }

        private static void CreateStorage(Transform parent)
        {
            var storage = new Color32(47, 55, 63, 255);
            CreateCube(parent, "Apartment Storage Cabinet", new Vector3(2.68f, 0.42f, 3.6f),
                new Vector3(1.18f, 0.82f, 0.68f), storage, true);
            CreateCube(parent, "Apartment Storage Cabinet Left Door", new Vector3(2.38f, 0.43f, 3.245f),
                new Vector3(0.52f, 0.66f, 0.035f), new Color32(57, 65, 73, 255), false);
            CreateCube(parent, "Apartment Storage Cabinet Right Door", new Vector3(2.96f, 0.43f, 3.245f),
                new Vector3(0.52f, 0.66f, 0.035f), new Color32(57, 65, 73, 255), false);
            CreateCube(parent, "Apartment Storage Cabinet Pull", new Vector3(2.68f, 0.45f, 3.215f),
                new Vector3(0.045f, 0.12f, 0.025f), new Color32(116, 127, 137, 255), false);
            CreateCube(parent, "Apartment Wall Shelf", new Vector3(2.68f, 1.34f, 3.78f),
                new Vector3(1.42f, 0.08f, 0.42f), new Color32(48, 56, 64, 255), true);
            CreateCube(parent, "Apartment Storage Box", new Vector3(2.35f, 1.63f, 3.78f),
                new Vector3(0.48f, 0.5f, 0.34f), new Color32(44, 62, 67, 255), true);
            CreateCube(parent, "Apartment Storage Box Label", new Vector3(2.35f, 1.65f, 3.598f),
                new Vector3(0.24f, 0.045f, 0.01f), new Color32(96, 150, 158, 255), false);
            CreateCube(parent, "Apartment Storage Box Small", new Vector3(2.94f, 1.55f, 3.78f),
                new Vector3(0.42f, 0.34f, 0.32f), new Color32(58, 66, 74, 255), true);
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
                if (material.HasProperty("_Smoothness"))
                {
                    material.SetFloat("_Smoothness", 0.12f);
                }

                if (material.HasProperty("_Glossiness"))
                {
                    material.SetFloat("_Glossiness", 0.12f);
                }

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
