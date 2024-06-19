using System.Collections;
using UnityEngine;

namespace Newvia {
    public abstract class StateBase : MonoBehaviour, CharacterState
    {
        protected CharacterInit _CharacterInit = null;//캐릭터 컨트롤러

        //기능 구현 부분
        public void Handle(CharacterInit init)
        {
            //캐릭터 값 초기화
            if (!this._CharacterInit)
                this._CharacterInit = init;
        }
    }
}
