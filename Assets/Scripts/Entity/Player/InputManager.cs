using UnityEngine;

/// <summary>
/// Used to read player input.
/// </summary>
internal class InputManager
{
    /// <summary>
    /// -1 if A held, 1 if D held, 0 if neither or both are held.
    /// </summary>
    public float moveValue { get { return ReadMoveInput(); } }
    /// <summary>
    /// Is the player pressing space?
    /// </summary>
    public bool jumpInput { get { return Input.GetKey(KeyCode.Space); } }
    public bool standardAttackInput { get { return Input.GetMouseButton(0); } }
    public bool specialAttackInput { get { return Input.GetMouseButton(1); } }

    private float ReadMoveInput()
    {
        float value = 0;
        if (Input.GetKey(KeyCode.A)) value--;
        if (Input.GetKey(KeyCode.D)) value++;
        return value;
    }
}