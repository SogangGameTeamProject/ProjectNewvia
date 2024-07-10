using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Newvia
{
    public class WeaponeHUDController : Observer
    {
        private Weapone _weapone;
        [SerializeField]
        private Image _magazineCapacityGauge;
        private float _maxMagazineCapacity;//최대 장탄 수
        private float _nowMagazineCapacity;//현재 장탄수

        public override void Notify(Subject subject)
        {
            if (!_weapone)
                _weapone = subject.GetComponent<Weapone>();

            if (_weapone)
            {
                _maxMagazineCapacity = _weapone._maxMagazineCapacity;
                _nowMagazineCapacity = _weapone.MagazineCapacity;

                _magazineCapacityGauge.fillAmount = _nowMagazineCapacity / _maxMagazineCapacity;
            }
        }
    }
}

