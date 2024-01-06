using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

public class Portal(
    Vector2 position,
    Rectangle bounds,
    int order,
    string orderDisplay,
    Line topSide,
    Line rightSide,
    Line bottomSide,
    Line leftSide
) : Cell(position, bounds) {
    public static readonly int ORDER_FONT_SIZE = 24;

    Vector2 orderDisplayPosition = new(
        position.X - MeasureText(orderDisplay, ORDER_FONT_SIZE) / 2,
        position.Y - ORDER_FONT_SIZE / 2 + 1
    );

    /*@(private="file") updatePortals :: proc(portals: []^Portal, playerProjectiles: []Projectile) {
        for i in 0..<len(portals) {
            for j in 0..<len(playerProjectiles) {
                if r.CheckCollisionPointRec(playerProjectiles[j].position, portals[i].bounds) && i < len(portals) - 1 {
                    // CheckCollisionLines. Get line of each side. Get line of trajectory and move to collision point
                } 
            }
        }
    }*/

    public void draw() {
        DrawRectangleLinesEx(
            bounds,
            1,
            WHITE
        );

        DrawText(
            orderDisplay,
            (int)orderDisplayPosition.X,
            (int)orderDisplayPosition.Y,
            ORDER_FONT_SIZE,
            WHITE
        );

        DrawLine((int)topSide.Start.X, (int)(topSide.Start.Y - 5), (int)topSide.End.X, (int)(topSide.End.Y - 5), RED);
    }
}
