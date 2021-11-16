using System;
using Desdiene.Types.ProcessContainers;
using Desdiene.Types.Processes;

namespace Desdiene.UI.Animators
{
    public class UiElementAnimations : IUiElementAnimation
    {
        private readonly IUiElementAnimation[] _animations;

        public UiElementAnimations(IUiElementAnimation[] animations)
        {
            _animations = animations ?? throw new ArgumentNullException(nameof(animations));
        }

        void IUiElementAnimation.Hide(Action OnEnded)
        {
            IProcesses processes = new ParallelProcesses("Ожидание выполнения всех анимаций скрытия UI элемента");

            for (int i = 0; i < _animations.Length; i++)
            {
                IProcess process = new OptionalLinearProcess($"{i}-я анимация скрытия UI элемента");
                process.Start();
                processes.Add(process);

                _animations[i].Hide(() =>
                {
                    process.Stop();
                });
            }

            processes.WhenCompleted += () =>
            {
                processes.Clear();
                OnEnded?.Invoke();
            };
        }

        void IUiElementAnimation.Show(Action OnEnded)
        {
            IProcesses processes = new ParallelProcesses("Ожидание выполнения всех анимаций показа UI элемента");

            for (int i = 0; i < _animations.Length; i++)
            {
                IProcess process = new OptionalLinearProcess($"{i}-я анимация показа UI элемента");
                process.Start();
                processes.Add(process);

                _animations[i].Show(() =>
                {
                    process.Stop();
                });
            }

            processes.WhenCompleted += () =>
            {
                processes.Clear();
                OnEnded?.Invoke();
            };
        }
    }
}
