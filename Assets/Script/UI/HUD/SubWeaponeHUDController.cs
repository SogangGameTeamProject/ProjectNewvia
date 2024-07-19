using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Newvia
{
    public class SubWeaponeHUDController : Observer
    {
        private SubWeapone _subWeapone = null;
        public int _maximumCharge { get; set; } // 최대 스킬 충전 횟수
        public int _energyConsumed { get; set; }       // 스킬 코스트
        public GameObject smallBarPrefab; // 작은 바의 프리팹
        public Sprite emptyBarLImage = null;
        public Sprite emptyBarMImage = null;
        public Sprite emptyBarRImage = null;
        public Sprite fillBarLImage = null;
        public Sprite fillBarMImage = null;
        public Sprite fillBarRImage = null;
        public Transform hudContainer;   // HUD를 담을 컨테이너

        public float smallBarSpacing = 2f; // 작은 바 간의 간격
        public float bigBarSpacing = 10f;  // 큰 바 간의 간격

        private int maxSkillGauge; // 최대 스킬 게이지

        public void CreateSkillHUD(SubWeapone subWeapone)
        {
            _subWeapone = subWeapone;
            _maximumCharge = _subWeapone._maximumCharge;
            _energyConsumed = _subWeapone._energyConsumed;
            maxSkillGauge = _maximumCharge * _energyConsumed;

            for (int i = 0; i < _maximumCharge; i++)
            {
                GameObject skillSegment = new GameObject("SkillSegment" + i);
                skillSegment.transform.SetParent(hudContainer);

                HorizontalLayoutGroup hlg = skillSegment.AddComponent<HorizontalLayoutGroup>();
                hlg.spacing = smallBarSpacing;
                hlg.childAlignment = TextAnchor.MiddleCenter;
                hlg.childControlWidth = false;
                hlg.childControlHeight = false;
                hlg.childForceExpandHeight = false;
                hlg.childForceExpandWidth = false;

                for (int j = 0; j < _energyConsumed; j++)
                {
                    Instantiate(smallBarPrefab, skillSegment.transform);
                }
            }

            HorizontalLayoutGroup hudHlg = hudContainer.gameObject.AddComponent<HorizontalLayoutGroup>();
            hudHlg.spacing = bigBarSpacing;
            hudHlg.childAlignment = TextAnchor.MiddleCenter;
            hudHlg.childForceExpandHeight = false;
            hudHlg.childForceExpandWidth = false;

            UpdateSkillHUD(_subWeapone._nowEnergy);
        }

        //현재 스킬 게이지 량에 맞추어 스킬게이지 바 이미지를 교체하는 부분
        public void UpdateSkillHUD(int currentGauge)
        {
            for (int i = 0; i < hudContainer.childCount; i++)
            {
                Transform skillSegment = hudContainer.GetChild(i);
                for (int j = 0; j < skillSegment.childCount; j++)
                {
                    
                    int thisGaugeNum = ((i) * _energyConsumed) + (j);
                    //현재 게이지 량에 따른 이미지 교체를 하는 부분
                    Sprite barSprite = null;
                    if (thisGaugeNum <= currentGauge-1)
                    {
                        if (j == 0)
                            barSprite = fillBarLImage;
                        else if (j == _energyConsumed - 1)
                            barSprite = fillBarRImage;
                        else
                            barSprite = fillBarMImage;
                    }
                    else
                    {
                        if (j == 0)
                            barSprite = emptyBarLImage;
                        else if (j == _energyConsumed - 1)
                            barSprite = emptyBarRImage;
                        else
                            barSprite = emptyBarMImage;
                    }

                    skillSegment.GetChild(j).GetComponent<Image>().sprite = barSprite;
                }
            }
        }
        public override void Notify(Subject subject)
        {
            UpdateSkillHUD(_subWeapone._nowEnergy);
        }
    }

}