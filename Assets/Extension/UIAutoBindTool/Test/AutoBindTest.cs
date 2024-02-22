using UnityEngine;

namespace Rabi
{
    public partial class AutoBindTest : MonoBehaviour
    {
        private int _count;

        private void Start()
        {
            GetBindComponents(gameObject);
            _btnTest2.onClick.AddListener(OnBtnClick);
        }

        private void OnBtnClick()
        {
            _count++;
            _txtTest3.text = "点击了按钮" + _count + "次";
        }
    }
}