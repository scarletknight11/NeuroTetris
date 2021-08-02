using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using brainflow;
using brainflow.math;


namespace test
{
    public class Cube2 : MonoBehaviour   { 
    
        static void Main (string[] args)
        {
            // use synthetic board for demo
            BoardShim.enable_dev_board_logger ();
            BrainFlowInputParams input_params = new BrainFlowInputParams ();
            int board_id = (int)BoardIds.SYNTHETIC_BOARD;

            BoardShim board_shim = new BoardShim (board_id, input_params);
            board_shim.prepare_session ();
            board_shim.start_stream (3600);
            System.Threading.Thread.Sleep (5000);
            board_shim.stop_stream ();
            double[,] unprocessed_data = board_shim.get_current_board_data (20);
            Debug.Log("Num elements: " + unprocessed_data.GetLength(1));
            int[] eeg_channels = BoardShim.get_eeg_channels (board_id);
            board_shim.release_session ();
            Debug.Log("hello");
            // for demo apply different filters to different channels
            double[] Channel0_Notchfiltered = null;
            double[] Channel1_Notchfiltered = null;
            double[] Channel2_Notchfiltered = null;
            double[] Channel3_Notchfiltered = null;
            for (int i = 0; i < eeg_channels.Length; i++)
            {
                Debug.Log("Before processing:");
                // Debug.Log("[{" + i + "}]", string.Join (", ", unprocessed_data.GetRow (eeg_channels[i])));
                switch (i)
                {
                    case 0:
                        Channel0_Notchfiltered = DataFilter.remove_environmental_noise(unprocessed_data.GetRow(eeg_channels[i]), BoardShim.get_sampling_rate(board_id), (int)NoiseTypes.SIXTY);
                        Debug.Log("NotchFiltered channel " + eeg_channels[i]);
                        // Debug.Log("[{0}]", string.Join(", ", Channel0_Notchfiltered));

                        break;
                    case 1:
                        Channel1_Notchfiltered = DataFilter.remove_environmental_noise(unprocessed_data.GetRow(eeg_channels[i]), BoardShim.get_sampling_rate(board_id), (int)NoiseTypes.SIXTY);
                        Debug.Log("NotchFiltered channel " + eeg_channels[i]);
                        // Debug.Log("[{1}]", string.Join(", ", Channel1_Notchfiltered));
                        break;
                    case 2:
                        Channel2_Notchfiltered = DataFilter.remove_environmental_noise(unprocessed_data.GetRow(eeg_channels[i]), BoardShim.get_sampling_rate(board_id), (int)NoiseTypes.SIXTY);
                        Debug.Log("NotchFiltered channel " + eeg_channels[i]);
                        // Debug.Log("[{2}]", string.Join(", ", Channel2_Notchfiltered));
                        break;
                    case 3:
                        Channel3_Notchfiltered = DataFilter.remove_environmental_noise(unprocessed_data.GetRow(eeg_channels[i]), BoardShim.get_sampling_rate(board_id), (int)NoiseTypes.SIXTY);
                        Debug.Log("NotchFiltered channel " + eeg_channels[i]);
                        // Debug.Log("[{3}]", string.Join(", ", Channel3_Notchfiltered));
                        break;
                }
            }
        }
    }
}
// filtered = DataFilter.perform_bandpass(unprocessed_data.GetRow(eeg_channels[i]), BoardShim.get_sampling_rate(board_id), 15.0, 5.0, 2, (int)FilterTypes.BUTTERWORTH, 0.0);
// Console.WriteLine("Filtered channel " + eeg_channels[i]);
// Console.WriteLine("[{0}]", string.Join(", ", filtered));
// break;