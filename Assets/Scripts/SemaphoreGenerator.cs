using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SemaphoreGenerator {

    public static List<SemaphoreGestureTarget> loadExistentSemaphoreGuestures()
    {
        List<SemaphoreGestureTarget> ls = new List<SemaphoreGestureTarget>();
        ls.Add(new SemaphoreGestureTarget("g01", "g01"));
        ls.Add(new SemaphoreGestureTarget("g01", "g10"));
        ls.Add(new SemaphoreGestureTarget("g01", "g21"));
        ls.Add(new SemaphoreGestureTarget("g02", "g22"));
        ls.Add(new SemaphoreGestureTarget("g10", "g10"));
        ls.Add(new SemaphoreGestureTarget("g10", "g21"));
        ls.Add(new SemaphoreGestureTarget("g21", "g21"));
        return ls;
    }
	
}
