
using UnityEngine;
using UnityEditor;

namespace Rabi
{
    public static class UIFactory
    {
        private static void CreateInstance(GameObject ui, MenuCommand menuCommand)
        {
            var parent = menuCommand.context as GameObject;
            if (!parent)
            {
	            return;
            }

            var go = Object.Instantiate(ui, parent.transform);
            go.name = ui.name;
            Selection.activeGameObject = go;
        }

		[MenuItem("GameObject/拉比UI/Button", false, 0)]
		private static void CreateButton(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/UI/Example/Button.prefab");
			CreateInstance(prefab, menuCommand);
		}

		[MenuItem("GameObject/拉比UI/EmptyButton", false, 0)]
		private static void CreateEmptyButton(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/UI/Example/EmptyButton.prefab");
			CreateInstance(prefab, menuCommand);
		}

		[MenuItem("GameObject/拉比UI/HListview", false, 0)]
		private static void CreateHorizontalListView(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/UI/Example/HorizontalListView.prefab");
			CreateInstance(prefab, menuCommand);
		}

		[MenuItem("GameObject/拉比UI/HStaticListView", false, 0)]
		private static void CreateHorizontalStaticListView(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/UI/Example/HorizontalStaticListView.prefab");
			CreateInstance(prefab, menuCommand);
		}

		[MenuItem("GameObject/拉比UI/Image", false, 0)]
		private static void CreateImage(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/UI/Example/Image.prefab");
			CreateInstance(prefab, menuCommand);
		}

		[MenuItem("GameObject/拉比UI/VListview", false, 0)]
		private static void CreateVerticalListView(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/UI/Example/VerticalListView.prefab");
			CreateInstance(prefab, menuCommand);
		}

		[MenuItem("GameObject/拉比UI/VStaticListView", false, 0)]
		private static void CreateVerticalStaticListView(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/UI/Example/VerticalStaticListView.prefab");
			CreateInstance(prefab, menuCommand);
		}

		[MenuItem("GameObject/拉比UI/StateController", false, 0)]
		private static void CreateStateController(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/UI/Example/StateController.prefab");
			CreateInstance(prefab, menuCommand);
		}

		[MenuItem("GameObject/拉比UI/Text", false, 0)]
		private static void CreateText(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/UI/Example/Text.prefab");
			CreateInstance(prefab, menuCommand);
		}
	}
}