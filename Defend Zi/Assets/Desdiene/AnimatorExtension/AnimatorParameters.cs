using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Desdiene.AnimatorExtension
{
    public class AnimatorParameters
    {
        private readonly AnimatorControllerParameter[] _parameters;

        public AnimatorParameters(Animator animator)
        {
            _parameters = animator.parameters;
        }

        public bool Has(string paramName, AnimatorControllerParameterType paramType)
        {
            foreach (AnimatorControllerParameter param in _parameters)
            {
                if (param.name == paramName && param.type == paramType)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
