using static Apple.CoreGraphics;

namespace PInvoke;

public static class Program
{
    private static unsafe void Main()
    {
        CGWindowListOption options = 0; // kCGWindowListOptionAll
        var x = CGWindowListCreate(options, kCGNullWindowID);
    }
}