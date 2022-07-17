using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationFSM {
	public struct Conditions {
		public State currentState;
		public bool isAnimationFinished;
		public bool isOnGround;
		public bool isAttacking;
		public Vector2Int aimDirection;
		public Vector2Int moveDirection;
	}

	public abstract class State {
		public int animationNameHash;

		protected State(int animationNameHash) {
			this.animationNameHash = animationNameHash;
		}

		public abstract bool IsMatchingConditions(in Conditions conditions);
	}

	public class FSM {
		public Conditions conditions;
		public State currentState;
		List<State> states = new List<State>();

		public void AddState(State state) {
			states.Add(state);
		}

		public void Update() {
			foreach (var state in states) {
				if (state.IsMatchingConditions(in conditions)) {
					currentState = state;
					break;
				}
			}
		}
	}
}
