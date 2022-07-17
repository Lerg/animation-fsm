using System.Runtime.CompilerServices;

namespace AnimationFSM {
	public class AttackRunState : State {
		public AttackRunState(int animationName) : base(animationName) {}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool IsMatchingConditions(in Conditions conditions) {
			return conditions.isAttacking && conditions.isOnGround && conditions.aimDirection.x != 0;
		}
	}

	public class AttackState : State {
		public AttackState(int animationName) : base(animationName) {}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool IsMatchingConditions(in Conditions conditions) {
			return conditions.isAttacking || (conditions.currentState == this && !conditions.isAnimationFinished);
		}
	}

	public class AttackUpState : State {
		public AttackUpState(int animationName) : base(animationName) {}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool IsMatchingConditions(in Conditions conditions) {
			return conditions.isAttacking && conditions.aimDirection.y == 1;
		}
	}

	public class DuckState : State {
		public DuckState(int animationName) : base(animationName) {}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool IsMatchingConditions(in Conditions conditions) {
			return conditions.isOnGround && conditions.aimDirection.y == -1;
		}
	}

	public class FallState : State {
		public FallState(int animationName) : base(animationName) {}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool IsMatchingConditions(in Conditions conditions) {
			return !conditions.isOnGround && conditions.moveDirection.y == -1;
		}
	}

	public class IdleState : State {
		public IdleState(int animationName) : base(animationName) {}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool IsMatchingConditions(in Conditions conditions) {
			return true;
		}
	}

	public class JumpState : State {
		public JumpState(int animationName) : base(animationName) {}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool IsMatchingConditions(in Conditions conditions) {
			return !conditions.isOnGround && conditions.moveDirection.y == 1;
		}
	}

	public class RunState : State {
		public RunState(int animationName) : base(animationName) {}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool IsMatchingConditions(in Conditions conditions) {
			return conditions.aimDirection.x != 0;
		}
	}
}