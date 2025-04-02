using UnityEngine;

namespace _1Core.Scripts.Tools.Extensions
{
    public static class AnimatorExtensions
    {
        public static float GetAnimationLength(this Animator animator, string animationName)
        {
            RuntimeAnimatorController ac = animator.runtimeAnimatorController;

            foreach (AnimationClip clip in ac.animationClips)
            {
                if (clip.name == animationName)
                {
                    return clip.length;
                }
            }

            return 0f;
        }

        public static float GetAnimationLengthWithSpeed(this Animator animator, string animationName)
        {
            RuntimeAnimatorController ac = animator.runtimeAnimatorController;

            float clipLength = 0f;
            foreach (AnimationClip clip in ac.animationClips)
            {
                if (clip.name == animationName)
                {
                    clipLength = clip.length;
                    break;
                }
            }

            if (clipLength == 0f)
                return 0f;

            float animatorSpeed = animator.speed;
            return clipLength / animatorSpeed;
        }
    }
}