﻿using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class PlayerAnimator : MonoBehaviourExt
{
    [SerializeField, NotNull] PlayerAuraAnimator _playerAura;

    public void ReinforceAure() => _playerAura.Reinforce();
}
