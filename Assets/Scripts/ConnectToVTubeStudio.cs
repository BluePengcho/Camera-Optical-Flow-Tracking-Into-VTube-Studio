using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTS.Core;

namespace VTS.Unity {

	public class ConnectToVTubeStudio : UnityVTSPlugin {

		[SerializeField]
		private Image _connectionLight = null;
		[SerializeField]
		private Text _connectionText = null;

        public static float EyeX = 0;
        public static float EyeY = 0;

        public static float FaceX = 0;
        public static float FaceY = 0;

        private void Awake() {
			Connect();
		}

		public void Connect() {
			this._connectionLight.color = Color.yellow;
			this._connectionText.text = "Connecting...";
			Initialize(new WebSocketSharpImpl(this.Logger), new NewtonsoftJsonUtilityImpl(), new TokenStorageImpl(Application.persistentDataPath),
			() => {
				this.Logger.Log("Connected!");
				this._connectionLight.color = Color.green;
				this._connectionText.text = "Connected!";
			},
			() => {
				this.Logger.LogWarning("Disconnected!");
				this._connectionLight.color = Color.gray;
				this._connectionText.text = "Disconnected.";
			},
			(error) => {
				this.Logger.LogError("Error! - " + error.data.message);
				this._connectionLight.color = Color.red;
				this._connectionText.text = "Error!";
			});
		}

		private void SyncValues(VTSParameterInjectionValue[] values) {
			InjectParameterValues(
				values,
				VTSInjectParameterMode.ADD,
				(r) => { },
				(e) => { this.Logger.LogError(e.data.message); }
			);
		}

		private void FixedUpdate() {

			if (this.IsAuthenticated) {
				SyncValues(new VTSParameterInjectionValue[] {
					new VTSParameterInjectionValue { id = "FaceAngleX", value = FaceX, weight = 1 },
					new VTSParameterInjectionValue { id = "FaceAngleY", value = FaceY, weight = 1 },
					new VTSParameterInjectionValue { id = "EyeRightX", value = EyeX, weight = 1 },
					new VTSParameterInjectionValue { id = "EyeRightY", value = EyeY, weight = 1 },
				});
			}
		}
	}
}
