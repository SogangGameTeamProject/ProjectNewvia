using System.Collections;
using UnityEngine;

namespace Newvia {
    public abstract class StateBase : MonoBehaviour, CharacterState
    {
        protected CharacterInit _CharacterInit = null;//ĳ���� ��Ʈ�ѷ�

        //��� ���� �κ�
        public void Handle(CharacterInit init)
        {
            //ĳ���� �� �ʱ�ȭ
            if (!this._CharacterInit)
                this._CharacterInit = init;
        }
    }
}
