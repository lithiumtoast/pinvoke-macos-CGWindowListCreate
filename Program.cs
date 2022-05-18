using static Apple.CoreGraphics;

// ReSharper disable InconsistentNaming

namespace PInvoke;

public static class Program
{
    private static unsafe void Main()
    {
        var kCGWindowNumber = CFString("kCGWindowNumber");
        var kCGWindowOwnerPID = CFString("kCGWindowOwnerPID");
        var kCGWindowOwnerName = CFString("kCGWindowOwnerName");
        var kCGWindowName = CFString("kCGWindowName");
        var kCGWindowIsOnscreen = CFString("kCGWindowIsOnscreen");
        var kCGWindowLayer = CFString("kCGWindowLayer");
        var kCGWindowMemoryUsage = CFString("kCGWindowMemoryUsage");

        var windowsIdentifiers = CGWindowListCreate(kCGWindowListOptionAll, kCGNullWindowID);
        var windowsDictionaries = CGWindowListCreateDescriptionFromArray(windowsIdentifiers);
        
        var windowsCount = CFArrayGetCount(windowsDictionaries);
        Console.WriteLine("Windows count: " + windowsCount);

        for (var i = 0; i < windowsCount; i++)
        {
            CFDictionaryRef dictionary = CFArrayGetValueAtIndex(windowsDictionaries, i);
            Console.WriteLine("Window index: " + i);
            
            var windowNumber = DictionaryReadSInt32(dictionary, kCGWindowNumber);
            Console.WriteLine("\tNumber: " + windowNumber);

            var windowOwnerPID = DictionaryReadCInt(dictionary, kCGWindowOwnerPID);
            Console.WriteLine("\tOwnerPID: " + windowOwnerPID);
            
            var windowOwnerName = DictionaryReadString(dictionary, kCGWindowOwnerName);
            Console.WriteLine("\tOwnerName: " + windowOwnerName);
            
            var windowName = DictionaryReadString(dictionary, kCGWindowName);
            Console.WriteLine("\tName: " + windowName);
            
            var windowIsOnScreen = DictionaryReadBool(dictionary, kCGWindowIsOnscreen) ?? false;
            Console.WriteLine("\tIsOnScreen: " + windowIsOnScreen);
            
            var windowLayer = DictionaryReadCInt(dictionary, kCGWindowLayer);
            Console.WriteLine("\tLayer: " + windowLayer);
            
            var memoryUsage = DictionaryReadLongLong(dictionary, kCGWindowMemoryUsage);
            Console.WriteLine("\tMemoryUsage: " + memoryUsage);
        }
        
        CFRelease(windowsIdentifiers);
        CFRelease(windowsDictionaries);
        
        CFRelease(kCGWindowNumber);
        CFRelease(kCGWindowOwnerPID);
        CFRelease(kCGWindowOwnerName);
        CFRelease(kCGWindowName);
        CFRelease(kCGWindowIsOnscreen);
        CFRelease(kCGWindowLayer);
        CFRelease(kCGWindowMemoryUsage);
    }

    private static unsafe int? DictionaryReadSInt32(CFDictionaryRef dictionary, CFStringRef key)
    {
        void* dictionaryValue;
        var containsKey = CFDictionaryGetValueIfPresent(dictionary, key, &dictionaryValue);
        if (!containsKey)
        {
            return null;
        }

        var number = (CFNumberRef)dictionaryValue;
        CFNumberType type;
        type.Data = kCFNumberSInt32Type;
        int value;
        CFNumberGetValue(number, type, &value);
        return value;
    }
    
    private static unsafe int? DictionaryReadCInt(CFDictionaryRef dictionary, CFStringRef key)
    {
        void* dictionaryValue;
        var containsKey = CFDictionaryGetValueIfPresent(dictionary, key, &dictionaryValue);
        if (!containsKey)
        {
            return null;
        }

        var number = (CFNumberRef)dictionaryValue;
        CFNumberType type;
        type.Data = kCFNumberIntType;
        int value;
        CFNumberGetValue(number, type, &value);
        return value;
    }
    
    private static unsafe long? DictionaryReadLongLong(CFDictionaryRef dictionary, CFStringRef key)
    {
        void* dictionaryValue;
        var containsKey = CFDictionaryGetValueIfPresent(dictionary, key, &dictionaryValue);
        if (!containsKey)
        {
            return null;
        }

        var number = (CFNumberRef)dictionaryValue;
        CFNumberType type;
        type.Data = kCFNumberLongLongType;
        long value;
        CFNumberGetValue(number, type, &value);
        return value;
    }

    private static unsafe string? DictionaryReadString(CFDictionaryRef dictionary, CFStringRef key)
    {
        void* dictionaryValue;
        var containsKey = CFDictionaryGetValueIfPresent(dictionary, key, &dictionaryValue);
        if (!containsKey)
        {
            return null;
        }

        var cfString = (CFStringRef)dictionaryValue;
        var cString = CFStringGetCStringPtr(cfString, default);
        return (string)cString;
    }
    
    private static unsafe bool? DictionaryReadBool(CFDictionaryRef dictionary, CFStringRef key)
    {
        void* dictionaryValue;
        var containsKey = CFDictionaryGetValueIfPresent(dictionary, key, &dictionaryValue);
        if (!containsKey)
        {
            return null;
        }
        
        var cfBool = (CFBooleanRef)dictionaryValue;
        var cBool = CFBooleanGetValue(cfBool);
        return (bool)cBool;
    }

    private static CFStringRef CFString(string value)
    {
        var cString = Runtime.CStrings.CString(value);
        return CFStringCreateWithCString(default, cString, default);
    }
}