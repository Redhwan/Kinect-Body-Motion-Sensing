using UnityEngine;
using System.Collections;

public class Directions {

    public enum direction {
        up,
        down,
        right,
        left
    }

    public static direction getRandomDirection() {

        return (direction)UnityEngine.Random.Range(0, 3);
    }
}
