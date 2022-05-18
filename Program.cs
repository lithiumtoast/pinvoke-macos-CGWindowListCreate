using static Apple.CoreGraphics;

namespace PInvoke;

public static class Program
{
    private static unsafe void Main()
    {
        var windowsIdentifiers = CGWindowListCreate(kCGWindowListOptionAll, kCGNullWindowID);
        var windowsData = CGWindowListCreateDescriptionFromArray(windowsIdentifiers);
        
        var windowsCount = CFArrayGetCount(windowsData);
        for (var i = 0; i < windowsCount; i++)
        {
            CFDictionaryRef dictionary = CFArrayGetValueAtIndex(windowsData, i);
            var dictionaryItemsCount = CFDictionaryGetCount(dictionary);
            for (var j = 0; j < dictionaryItemsCount; j++)
            {
                // TODO: Need the key variable as shown here: https://developer.apple.com/documentation/coregraphics/quartz_window_services/required_window_list_keys?language=objc
                // var value = CFDictionaryGetValue(dictionary, key);
            }
        }
    }
}