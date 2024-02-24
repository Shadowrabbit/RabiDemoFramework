
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

		[MenuItem("GameObject/拉比UI/Btn", false, 0)]
		private static void CreateButton(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AddressableAssets/MixUI/Example/Button.prefab");
			CreateInstance(prefab, menuCommand);
		}

		[MenuItem("GameObject/拉比UI/EmptyButton", false, 0)]
		private static void CreateEmptyButton(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AddressableAssets/MixUI/Example/EmptyButton.prefab");
			CreateInstance(prefab, menuCommand);
		}

		[MenuItem("GameObject/拉比UI/LvHorizontal", false, 0)]
		private static void CreateHorizontalListView(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AddressableAssets/MixUI/Example/HorizontalListView.prefab");
			CreateInstance(prefab, menuCommand);
		}

		[MenuItem("GameObject/拉比UI/SlvHorizontal", false, 0)]
		private static void CreateHorizontalStaticListView(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AddressableAssets/MixUI/Example/HorizontalStaticListView.prefab");
			CreateInstance(prefab, menuCommand);
		}

		[MenuItem("GameObject/拉比UI/Img", false, 0)]
		private static void CreateImage(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AddressableAssets/MixUI/Example/Image.prefab");
			CreateInstance(prefab, menuCommand);
		}

		[MenuItem("GameObject/拉比UI/TransImg", false, 0)]
		private static void CreateTransImage(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AddressableAssets/MixUI/Example/TransImage.prefab");
			CreateInstance(prefab, menuCommand);
		}

		[MenuItem("GameObject/拉比UI/TransTxt", false, 0)]
		private static void CreateTransText(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AddressableAssets/MixUI/Example/TransText.prefab");
			CreateInstance(prefab, menuCommand);
		}

		[MenuItem("GameObject/拉比UI/LvVertical", false, 0)]
		private static void CreateVerticalListView(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AddressableAssets/MixUI/Example/VerticalListView.prefab");
			CreateInstance(prefab, menuCommand);
		}

		[MenuItem("GameObject/拉比UI/SlvVertical", false, 0)]
		private static void CreateVerticalStaticListView(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AddressableAssets/MixUI/Example/VerticalStaticListView.prefab");
			CreateInstance(prefab, menuCommand);
		}

		[MenuItem("GameObject/拉比UI/TransMp", false, 0)]
		private static void CreateTransMeshPro(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AddressableAssets/MixUI/Example/TransMeshPro.prefab");
			CreateInstance(prefab, menuCommand);
		}

		[MenuItem("GameObject/拉比UI/PlotMp", false, 0)]
		private static void CreatePlotMeshPro(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AddressableAssets/MixUI/Example/PlotMeshPro.prefab");
			CreateInstance(prefab, menuCommand);
		}

		[MenuItem("GameObject/拉比UI/Sc", false, 0)]
		private static void CreateStateController(MenuCommand menuCommand)
		{
			var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AddressableAssets/MixUI/Example/StateController.prefab");
			CreateInstance(prefab, menuCommand);
		}
	}
}