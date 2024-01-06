using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;

public class Projectile {
    private static int PROJECTILE_SIZE = 6;
    private static int PROJECTILE_SPEED = 700;
    private Vector2 position;
    private Vector2 velocity;

    public Projectile(Vector2 launchPoint, Vector2 targetPoint) {
        float dx = targetPoint.X - launchPoint.X;
        float dy = targetPoint.Y - launchPoint.Y;
        
        double firingAngle = Math.Atan2(dy, dx);

        double vx = PROJECTILE_SPEED * Math.Cos(firingAngle);
        double vy = PROJECTILE_SPEED * Math.Sin(firingAngle);

        position = launchPoint;
        velocity = new Vector2((float)vx, (float)vy);
    }

    public static void updateProjectiles(List<Projectile> projectiles) {
        float delta = GetFrameTime();

        for (int i = 0; i < projectiles.Count; i++) {
            Projectile projectile = projectiles[i];

            projectile.position.X += projectile.velocity.X * delta;
            projectile.position.Y += projectile.velocity.Y * delta;

            if (projectile.position.X > Room.ROOM_END_X || projectile.position.X < Room.ROOM_START_X ||
                projectile.position.Y > Room.ROOM_END_Y || projectile.position.Y < Room.ROOM_START_Y) {
                projectiles.RemoveAt(i);
            }
        }
    }

    public void draw() {
        DrawCircle((int)position.X, (int)position.Y, PROJECTILE_SIZE, RED);
    }
}
