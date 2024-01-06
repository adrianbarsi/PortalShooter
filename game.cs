using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

public class Game {
    public static readonly int WINDOW_WIDTH = 800;
    public static readonly int WINDOW_HEIGHT = 800;
    public static readonly int WINDOW_PADDING = 60;

    Player player;
    Room room;
        
    public Game() {
        InitWindow(WINDOW_WIDTH, WINDOW_HEIGHT, "Portal Shooter");
        SetTargetFPS(60);
        HideCursor();

        player = new Player();
        room = new Room(player);
    }

    public void update() {
        room.update();
    }

    public void draw() {
        BeginDrawing();
            ClearBackground(BLACK);
            room.draw();
        EndDrawing();
    }

    public void cleanup() {
        CloseWindow();
    }
}
