using NUnit.Framework;
using UnityEngine;

namespace GUISystem.Tests
{
    /// <summary>
    /// TransformStateの挙動を検証するテストです。
    /// </summary>
    public class TransformStateTests
    {
        [Test]
        /// <summary>
        /// LocalPosition変更を検知できるかを検証します。
        /// </summary>

        public void DetectsPositionChange()
        {
            GameObject go = new GameObject("test");
            TransformState state = new TransformState(go.transform);
            go.transform.localPosition = new Vector3(1f, 0f, 0f);
            Assert.IsTrue(state.HasChanged(go.transform));
            Object.DestroyImmediate(go);
        }

        [Test]
        /// <summary>
        /// Capture後にHasChangedがfalseになることを確認します。
        /// </summary>
        public void CaptureUpdatesValues()
        {
            GameObject go = new GameObject("test");
            TransformState state = new TransformState(go.transform);
            go.transform.localPosition = new Vector3(1f, 0f, 0f);
            state.Capture(go.transform);
            Assert.IsFalse(state.HasChanged(go.transform));
            Object.DestroyImmediate(go);
        }
    }
}
