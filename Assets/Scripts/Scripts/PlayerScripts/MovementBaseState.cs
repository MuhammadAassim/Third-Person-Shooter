public abstract class MovementBaseState
{
    // Player Movement Ki State Mein Enter Karne Wala Function
    public abstract void EnterState(PlayerMovement movement);

    // Player Movement Ki State Ko Update Karne Wala Function
    public abstract void UpdateState(PlayerMovement movement);

    // Player Movement Ki State Ko Exit Karne Wala Function
    public abstract void ExitState(PlayerMovement movement, MovementBaseState state);
}
