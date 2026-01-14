namespace NYR.core;

internal class Camera
{
    private Camera3D _camera3D;
    public Vector3 Target { get; set; }
    public Vector3 CameraPosition { get; set; }
    public Vector3 Up { get; set; }
    public float Fov { get; set; }
    public Camera()
    {
        _camera3D = new Camera3D();
    }

    public void Begin()
    {

        _camera3D.Target = this.Target;
        _camera3D.Position = this.CameraPosition;
        _camera3D.Up = this.Up;
        _camera3D.FovY = this.Fov;
        _camera3D.Projection = CameraProjection.Orthographic;

        BeginMode3D(_camera3D);
    }

    public void End()
    {
        EndMode3D();
    }
}
