using UnityEngine;

public struct RectInt3D
{
    public RectInt3D(int x, int y, int z, int width, int height, int depth)
    {
        this.x = x;
        this.y = y;
        this.z = z;

        this.width = width;
        this.height = height;
        this.depth = depth;
    }

    public int x;
    public int y;
    public int z;

    private int width;
    private int height;
    private int depth;

    public int Width { get => x + width; }
    public int Height { get => y + height; }
    public int Depth { get => z + depth; }

    public RectInt3D Inside { get => new RectInt3D(x + 1, y + 1, z + 1, width - 2, height - 2, depth - 2); }
    public RectInt3D Outside { get => new RectInt3D(x - 1, y - 1, z - 1, width + 2, height + 2, depth + 2); }

    public bool Contains(Vector3 point)
    {
        //check if point is left of rect 
        if (point.x < x)
            return false;

        //check if point is right of rect 
        if (point.x > Width)
            return false;

        //check if point is below rect 
        if (point.y < y)
            return false;

        //check if point is above rect 
        if (point.y > Height)
            return false;

        //check if point is in front of rect 
        if (point.z < z)
            return false;

        //check if point is behind rect 
        if (point.z > Depth)
            return false;

        return true;
    }

    public bool Contains(RectInt3D rect)
    {
        //Front
        Vector3Int leftBottomFront = new Vector3Int(rect.x, rect.y, rect.Depth);
        if (Contains(leftBottomFront) == false)
            return false;

        Vector3Int rightBottomFront = new Vector3Int(rect.Width, rect.y, rect.Depth);
        if (Contains(rightBottomFront) == false)
            return false;

        Vector3Int leftTopFront = new Vector3Int(rect.x, rect.Height, rect.Depth);
        if (Contains(leftTopFront) == false)
            return false;

        Vector3Int rightTopFront = new Vector3Int(rect.Width, rect.Height, rect.Depth);
        if (Contains(rightTopFront) == false)
            return false;

        //Back
        Vector3Int leftBottomBack = new Vector3Int(rect.x, rect.y, rect.z);
        if (Contains(leftBottomBack) == false)
            return false;

        Vector3Int rightBottomBack = new Vector3Int(rect.Width, rect.y, rect.z);
        if (Contains(rightBottomBack) == false)
            return false;

        Vector3Int leftTopBack = new Vector3Int(rect.x, rect.Height, rect.z);
        if (Contains(leftTopBack) == false)
            return false;

        Vector3Int rightTopBack = new Vector3Int(rect.Width, rect.Height, rect.z);
        if (Contains(rightTopBack) == false)
            return false;

        return true;
    }

    public bool overlaps(RectInt3D otherRect)
    {
        //check if secondRect is left of firstRect 
        if (x > otherRect.Width)
            return false;

        //check if secondRect is right of firstRect 
        if (Width < otherRect.x)
            return false;

        //check if secondRect is below firstRect 
        if (y > otherRect.Height)
            return false;

        //check if secondRect is above firstRect 
        if (Height < otherRect.y)
            return false;

        //check if secondRect is behind firstRect 
        if (z > otherRect.Depth)
            return false;

        //check if secondRect is in front of firstRect 
        if (Depth < otherRect.z)
            return false;

        return true;
    }

    public RectInt3D GetWorldRect(Vector3Int offset)
    {
        int worldX = x + offset.x;
        int worldY = y + offset.y;
        int worldZ = z + offset.z;

        return new RectInt3D(worldX, worldY, worldZ, width, height, depth);
    }

    public Vector3Int GetWorldRectCenter(Vector3Int offset)
    {
        RectInt3D worldRect = GetWorldRect(offset);
        Vector3Int center = Vector3Int.zero;

        center.x = worldRect.x + (width / 2);
        center.y = worldRect.y + (height / 2);
        center.z = worldRect.z + (depth / 2);

        return center;
    }
}

