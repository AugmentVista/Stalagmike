using UnityEngine;

/// <summary>
/// Used to read player input.
/// </summary>
internal static class InputManager
{
    /// <summary>
    /// -1 if A held, 1 if D held, 0 if neither or both are held.
    /// </summary>
    public static float moveValue { get { return ReadMoveInput(); } }
    /// <summary>
    /// Is the player pressing space?
    /// </summary>
    public static bool jumpInput { get { return Input.GetKey(KeyCode.Space); } }
    public static bool standardAttackInput { get { return Input.GetMouseButton(0); } }
    public static bool specialAttackInput { get { return Input.GetMouseButton(1); } }
    public static bool pauseInput { get { return Input.GetKeyDown(KeyCode.Escape); } }

    private static float ReadMoveInput()
    {
        float value = 0;
        if (Input.GetKey(KeyCode.A)) value--;
        if (Input.GetKey(KeyCode.D)) value++;
        return value;
    }
}