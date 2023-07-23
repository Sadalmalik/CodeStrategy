using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace TestUnits
{
	public class GameController : MonoBehaviour
	{
		public  TMP_Text      infoLabel;
		private StringBuilder sb;

		[Header("Exit")]
		// Exit
		public int exitCounter;

		[Header("FPS")]
		// FPS
		private int   frames;
		private float nextFPSCheck;
		private int   framesCounter;

		[Header("Test units")]
		// Test Units
		public  int         unitsTarget;
		public  int         targetsCountStep = 100;
		public  float       spawnDelay;
		public  BugsUpdater updater;
		public  GameObject  unitPrefab;
		private float       _nextSpawn;
		
		private void Start()
		{
			sb = new StringBuilder();
		}

		private void Update()
		{
			UpdateExit();
			UpdateFPS();
			UpdateSpawn();

			BuildInfo();
		}

		private void UpdateExit()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				exitCounter--;
				if (exitCounter == 0)
					Application.Quit();
			}
		}

		private void UpdateFPS()
		{
			framesCounter++;
			if (Time.unscaledTime > nextFPSCheck)
			{
				nextFPSCheck  += 1;
				frames        =  framesCounter;
				framesCounter =  0;
			}
		}

		private void UpdateSpawn()
		{
			if (updater.bugs.Count < unitsTarget)
			{
				if (_nextSpawn < Time.time)
				{
					_nextSpawn = Time.time + spawnDelay;
					var bugInstance = GameObject.Instantiate(unitPrefab);
					var bug = bugInstance.GetComponentInChildren<Bug>();
					updater.bugs.Add(bug);
				}
			}

			if (Input.GetKeyDown(KeyCode.Tab))
			{
				unitsTarget += targetsCountStep;
			}
		}

		private void BuildInfo()
		{
			sb.Clear();

			sb.Append("FPS: ")
				.Append(frames)
				.Append(" | ")
				.Append($"{(1 / Time.unscaledDeltaTime):0.000}");
			sb.AppendLine();
			sb.Append("Press ESC ")
				.Append(exitCounter)
				.Append(" times to exit");
			sb.AppendLine();
			sb.Append("Units: ")
				.Append(updater.bugs.Count)
				.Append(" / ")
				.Append(unitsTarget);
			sb.AppendLine();
			sb.AppendLine("Press TAB to increase target count!");

			infoLabel.SetText(sb.ToString());
		}
	}
}