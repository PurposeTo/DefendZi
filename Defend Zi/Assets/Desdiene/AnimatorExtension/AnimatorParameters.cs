using System;
using System.Collections.Generic;
using UnityEngine;

namespace Desdiene.AnimatorExtension
{
    public class AnimatorParameters
    {
        private readonly Animator _unityAnimator;
        private readonly Dictionary<AnimatorControllerParameterType, Dictionary<string, AnimatorControllerParameter>> _parameters;

        public AnimatorParameters(Animator unityAnimator)
        {
            _unityAnimator = unityAnimator ?? throw new ArgumentNullException(nameof(unityAnimator));
            if (!_unityAnimator.isActiveAndEnabled) throw new InvalidOperationException($"Аниматор {_unityAnimator.name} должен быть включен для работы с ним");

            _parameters = new Dictionary<AnimatorControllerParameterType, Dictionary<string, AnimatorControllerParameter>>();
            Array.ForEach(Common.GetAllEnumValues<AnimatorControllerParameterType>(), (type) =>
            {
                _parameters.Add(type, new Dictionary<string, AnimatorControllerParameter>());
            });

            foreach (AnimatorControllerParameter param in _unityAnimator.parameters)
            {
                Dictionary<string, AnimatorControllerParameter> typedParameters = _parameters[param.type];
                typedParameters.Add(param.name, param);
            }
        }

        public bool Has(string paramName, AnimatorControllerParameterType paramType, out AnimatorControllerParameter parameter)
        {
            Dictionary<string, AnimatorControllerParameter> typedParameters = _parameters[paramType];
            bool has = typedParameters.ContainsKey(paramName);
            parameter = has
                ? typedParameters[paramName]
                : null;
            return has;
        }

        public void ResetAllTriggers()
        {
            Dictionary<string, AnimatorControllerParameter> triggers = _parameters[AnimatorControllerParameterType.Trigger];
            foreach (KeyValuePair<string, AnimatorControllerParameter> entry in triggers)
            {
                string fieldName = entry.Key;
                _unityAnimator.ResetTrigger(fieldName);
            }
        }
    }
}
