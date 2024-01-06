using static Raylib_cs.KeyboardKey;
using static Raylib_cs.MouseButton;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
using Raylib_cs;

public class Player {
    public static readonly int PLAYER_RADIUS = 20;
    static readonly int CROSSHAIRS_OUTER_CIRCLE_RADIUS = 7;
    static readonly int CROSSHAIRS_INNER_CIRCLE_RADIUS = 2;
    static readonly int PLAYER_SPEED = 300;

    Vector2 position = new(0, 0);
    Vector2 crosshairs = new(0, 0);
    List<Projectile> projectiles = new();
    
    public Vector2 Position { get => position; set => position = value; }

    void moveCharacter() {
        double speed = GetFrameTime() * PLAYER_SPEED;

        double dx = 0.0;
        double dy = 0.0;

        if (IsKeyDown(KEY_A)) {
            dx -= 1.0;
        }
        if (IsKeyDown(KEY_D)) {
            dx += 1.0;
        }
        if (IsKeyDown(KEY_W)) {
            dy -= 1.0;
        }
        if (IsKeyDown(KEY_S)) {
            dy += 1.0;
        }

        if (dx != 0.0 || dy != 0.0) {
            double length = Math.Sqrt(dx*dx + dy*dy);
            dx = dx/length * speed;
            dy = dy/length * speed;

            position.X = (float)Math.Clamp(position.X + dx, Room.ROOM_START_X + PLAYER_RADIUS + 1, Room.ROOM_END_X - PLAYER_RADIUS - 1);
            position.Y = (float)Math.Clamp(position.Y + dy, Room.ROOM_START_Y + PLAYER_RADIUS + 1, Room.ROOM_END_Y - PLAYER_RADIUS - 1);
        }
    }

    void moveCrosshairs() {
        Vector2 mousePosition = GetMousePosition();

        if (mousePosition.X == 0 && mousePosition.Y == 0) {
            return;
        }

        crosshairs.X = Math.Clamp(mousePosition.X, CROSSHAIRS_OUTER_CIRCLE_RADIUS, Game.WINDOW_WIDTH - CROSSHAIRS_OUTER_CIRCLE_RADIUS);
        crosshairs.Y = Math.Clamp(mousePosition.Y, CROSSHAIRS_OUTER_CIRCLE_RADIUS, Game.WINDOW_HEIGHT - CROSSHAIRS_OUTER_CIRCLE_RADIUS);
    }

    void shootProjectile() {
        if (IsMouseButtonPressed(MOUSE_BUTTON_LEFT)) {
            projectiles.Add(new Projectile(position, crosshairs));
        }
    }

    public void update() {
        moveCharacter();
        moveCrosshairs();
        shootProjectile();
        Projectile.updateProjectiles(projectiles);
    }

    void drawCharacter() {
        DrawCircleLines(
            (int)position.X,
            (int)position.Y,
            PLAYER_RADIUS,
            GREEN
        );
    }

    void drawCrossHairs() {
        if (crosshairs.X == 0 && crosshairs.Y == 0) {
            return;
        }

        DrawCircleLines(
            (int)crosshairs.X,
            (int)crosshairs.Y,
            CROSSHAIRS_OUTER_CIRCLE_RADIUS,
            RED
        );

        DrawCircle(
            (int)crosshairs.X,
            (int)crosshairs.Y,
            CROSSHAIRS_INNER_CIRCLE_RADIUS,
            RED
        );

        // Vertical Plus Line
        DrawLine(
            (int)crosshairs.X,
            (int)crosshairs.Y - CROSSHAIRS_OUTER_CIRCLE_RADIUS,
            (int)crosshairs.X,
            (int)crosshairs.Y + CROSSHAIRS_OUTER_CIRCLE_RADIUS,
            RED
        );

        // Horizontal Plus Line
        DrawLine(
            (int)crosshairs.X - CROSSHAIRS_OUTER_CIRCLE_RADIUS,
            (int)crosshairs.Y,
            (int)crosshairs.X + CROSSHAIRS_OUTER_CIRCLE_RADIUS,
            (int)crosshairs.Y,
            RED
        );
    }

    void drawProjectiles() {
        foreach(Projectile projectile in projectiles) {
            projectile.draw();
        }
    }

    public void draw() {
        drawCharacter();
        drawCrossHairs();
        drawProjectiles();
    }
}
