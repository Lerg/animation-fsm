# Animation FSM
Animation system for Unity or other C# game engines like Godot.

It's based on a simple Finite State Machine. All states are organizied in a priority queue, basically an array of states with the first state having the highest priority.

You can watch video description on my YouTube channel

https://www.youtube.com/channel/UCjkECP_YgfCXd6Y3j3rkY_g

## Porting to Godot or other C# engine

There is a dependency on Unity's Vector2Int class. Use an alternative in other engines. For Godot it's [Vector2i](https://github.com/godotengine/godot/blob/master/modules/mono/glue/GodotSharp/GodotSharp/Core/Vector2i.cs)

## Usage

### Setup

Define your game conditions for the animation system in the `AnimationFSM.Conditions` struct.

```csharp
public struct Conditions {
  public State currentState;
  public bool isAnimationFinished;
  public bool isOnGround;
  public bool isAttacking;
  public Vector2Int aimDirection;
  public Vector2Int moveDirection;
}
```

Define your animation states in the `AnimationStates.cs` file or anywhere else.

```csharp
public class RunState : State {
  public RunState(int animationName) : base(animationName) {}
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public override bool IsMatchingConditions(in Conditions conditions) {
    return conditions.aimDirection.x != 0;
  }
}
```

Each state should return `true` inside `IsMatchingConditions` when you wish for the animation to play.

Create an instance of this FSM in your player class as a field.

```csharp
AnimationFSM.FSM fsm = new AnimationFSM.FSM();
```

During the initialization add all the animation states in the order of their priority. And provide actual animation names for each state.

```csharp
fsm.AddState(new AnimationFSM.JumpState(Animator.StringToHash("PlayerJump")));
fsm.AddState(new AnimationFSM.FallState(Animator.StringToHash("PlayerFall")));
fsm.AddState(new AnimationFSM.AttackUpState(Animator.StringToHash("PlayerAttackUp")));
fsm.AddState(new AnimationFSM.AttackRunState(Animator.StringToHash("PlayerAttackRun")));
fsm.AddState(new AnimationFSM.AttackState(Animator.StringToHash("PlayerAttack")));
fsm.AddState(new AnimationFSM.RunState(Animator.StringToHash("PlayerRun")));
fsm.AddState(new AnimationFSM.DuckState(Animator.StringToHash("PlayerDuck")));
fsm.AddState(new AnimationFSM.IdleState(Animator.StringToHash("PlayerIdle")));
```

For performance animation names are converted into hashes.

### Game loop

In the update routine of your game set all the conditions for the FSM.

```csharp
var animatorState = animator.GetCurrentAnimatorStateInfo(0);
var sensitivity = 0.1f;
var velocity = rigidBody.velocity;

fsm.conditions.currentState = fsm.currentState;
fsm.conditions.isAnimationFinished = animatorState.normalizedTime >= 1f;
fsm.conditions.isOnGround = isOnGround;
fsm.conditions.isAttacking = Input.GetButton("Fire1");
fsm.conditions.aimDirection.x = (int)Mathf.Round(Input.GetAxisRaw("Horizontal"));
fsm.conditions.aimDirection.y = (int)Mathf.Round(Input.GetAxisRaw("Vertical"));
fsm.conditions.moveDirection.x = (velocity.x > sensitivity ? 1 : 0) + (velocity.x < -sensitivity ? -1 : 0);
fsm.conditions.moveDirection.y = (velocity.y > sensitivity ? 1 : 0) + (velocity.y < -sensitivity ? -1 : 0);
```

Run the state machine to determine which animation state is now active.

```csharp
fsm.Update();
```

And set the animation for your character accordingly.

```csharp
var fsmAnimationNameHash = fsm.currentState.animationNameHash;
if (animatorState.shortNameHash != fsmAnimationNameHash) {
  animator.Play(fsmAnimationNameHash);
}
```
