using Raylib_cs;
using System.Diagnostics;
using System.Reflection.Emit;
namespace readygame;

public class Entry
{
    public static void Main()
    {
        //Monitor shit
        SetConfigFlags(ConfigFlags.UndecoratedWindow | ConfigFlags.VSyncHint);
        int monitor = GetCurrentMonitor();
        InitWindow(GetMonitorWidth(monitor), GetMonitorHeight(monitor), "3D TEST");
        SetTargetFPS(GetMonitorRefreshRate(monitor));

        //Camera
        Camera3D camera = new Camera3D()
        {
            //Isometric
            Position = new(20, 20, 20),
            Up = new(0, 1, 0),
            Projection = CameraProjection.Orthographic,
            Target = new(0, 0, 0),
            FovY = 40f,
        };

        int descaling = 1;

        RenderTexture2D texture = LoadRenderTexture(GetMonitorWidth(monitor) / descaling, GetMonitorHeight(monitor) / descaling);
        SetTextureFilter(texture.Texture, TextureFilter.Point);

        Mesh a = GenMeshPlane(3f, 3f, 2, 2);
        Model b = LoadModelFromMesh(a);

        while (!WindowShouldClose())
        {
            int monitorW = GetMonitorWidth(monitor);
            int monitorH = GetMonitorHeight(monitor);


            //- ZOOM -------------------------------
            float wheel = GetMouseWheelMove();
            float currentFov = camera.FovY;
            float progress = 0.5f;
            
            if (wheel != 0)
            {
                float finalFov = camera.FovY - wheel;
                finalFov = Math.Clamp(finalFov, 1.0f, 90.0f);
                camera.FovY = (1 - progress) * currentFov + progress * finalFov; 
            }
            
            


            //- LOAD AND SET TEXTURES -------------------------------
            BeginTextureMode(texture);
            ClearBackground(Color.Black);
            BeginMode3D(camera);
            //X+
            DrawCubeV(new(3f, 0, 0), new(3f, 0f, 3f), Color.Red);
            //Z+
            DrawCubeV(new(0, 0, 3f), new(3f, 0f, 3f), Color.Green);
            DrawModel(b, new(0, 0, 0), 1.0f, Color.Blue);
            EndMode3D();
            EndTextureMode();


            //- DRAW TEXTURES -------------------------------
            BeginDrawing();
            ClearBackground(Color.Black);

            DrawTexturePro(texture.Texture,
                new(0, 0, texture.Texture.Width, texture.Texture.Height),
                new(0, 0, monitorW, monitorH),
                Vector2.Zero,
                0f, Color.White);

            //- DEBUG -------------------------------
            DrawText($"{monitorW}, {monitorH}", 20, 20, 20, Color.White);
            DrawFPS(20, 40);
            DrawText($"Texture: {texture.Texture.Width}, {texture.Texture.Height}", 20, 60, 20, ColorAlpha(Color.Red, 1f));
            DrawText($"MouseWheel: {GetMouseWheelMove()}", 20, 80, 20, ColorAlpha(Color.Red, 1f));
            DrawText($"FovY: {camera.FovY}", 20, 100, 20, ColorAlpha(Color.Red, 1f));
            DrawText($"FovY: {wheelTime}", 20, 120, 20, ColorAlpha(Color.Red, 1f));
            EndDrawing();
            
        }

        UnloadTexture(texture.Texture);
        CloseWindow();
    }
}
