using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using brainflow;
using brainflow.math;

public class Sphere : MonoBehaviour
{
    private BoardShim board_shim = null;
    private int sampling_rate = 0;

    // Start is called before the first frame update
    void Start()
    {
        try
        { 
            BoardShim.enable_dev_board_logger();

            BrainFlowInputParams input_params = new BrainFlowInputParams();
            int board_id = (int)BoardIds.SYNTHETIC_BOARD;
            board_shim = new BoardShim(board_id, input_params);
            board_shim.prepare_session();
            // BoardShim.set_log_file("brainflow_log.txt");
            board_shim.start_stream(450000, "file://brainflow_data.csv:w");
            sampling_rate = BoardShim.get_sampling_rate(board_id);
            Debug.Log("Brainflow streaming was started");
            double[,] unprocessed_data = board_shim.get_current_board_data(20);

            Debug.Log("Num elements: " + unprocessed_data.GetLength(1));
            int[] eeg_channels = BoardShim.get_eeg_channels(board_id);
            // for (int i = 0; i < unprocessed_data.GetLength(1); i++)
            // {
            //     print("data" + unprocessed_data[i, 1]);
            // }
            board_shim.release_session();
            print("hello");
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
                        Debug.Log("[{0}]" + ", " + Channel0_Notchfiltered);
                        // alpha bandpass filter
                        double[] AlphaChannel0 = DataFilter.perform_bandpass(Channel0_Notchfiltered, BoardShim.get_sampling_rate(board_id), 10.5, 5.0, 4, (int)FilterTypes.BUTTERWORTH, 0.0);
                        Debug.Log("Filtered channel " + (AlphaChannel0));
                        // beta bandpass filter
                        double[] BetaChannel0 = DataFilter.perform_bandpass(Channel0_Notchfiltered, BoardShim.get_sampling_rate(board_id), 21.5, 17.0, 4, (int)FilterTypes.BUTTERWORTH, 0.0);
                        Debug.Log("Filtered channel " + (BetaChannel0));
                        break;
                    case 1:
                        Channel1_Notchfiltered = DataFilter.remove_environmental_noise(unprocessed_data.GetRow(eeg_channels[i]), BoardShim.get_sampling_rate(board_id), (int)NoiseTypes.SIXTY);
                        Debug.Log("NotchFiltered channel " + eeg_channels[i]);
                        Debug.Log("[{1}]" + ", " + Channel1_Notchfiltered);
                        // alpha bandpass filter
                        double[] AlphaChannel1 = DataFilter.perform_bandpass(Channel1_Notchfiltered, BoardShim.get_sampling_rate(board_id), 10.5, 5.0, 4, (int)FilterTypes.BUTTERWORTH, 0.0);
                        Debug.Log("Filtered channel " + AlphaChannel1);
                        // beta bandpass filter
                        double[] BetaChannel1 = DataFilter.perform_bandpass(Channel1_Notchfiltered, BoardShim.get_sampling_rate(board_id), 21.5, 17.0, 4, (int)FilterTypes.BUTTERWORTH, 0.0);
                        Debug.Log("Filtered channel " + (BetaChannel1));
                        break;
                    case 2:
                        Channel2_Notchfiltered = DataFilter.remove_environmental_noise(unprocessed_data.GetRow(eeg_channels[i]), BoardShim.get_sampling_rate(board_id), (int)NoiseTypes.SIXTY);
                        Debug.Log("NotchFiltered channel " + eeg_channels[i]);
                        Debug.Log("[{2}]" + ", " + Channel2_Notchfiltered);
                        // alpha bandpass filter
                        double[] AlphaChannel2 = DataFilter.perform_bandpass(Channel2_Notchfiltered, BoardShim.get_sampling_rate(board_id), 10.5, 5.0, 4, (int)FilterTypes.BUTTERWORTH, 0.0);
                        Debug.Log("Filtered channel " + AlphaChannel2);
                        // beta bandpass filter
                        double[] BetaChannel2 = DataFilter.perform_bandpass(Channel2_Notchfiltered, BoardShim.get_sampling_rate(board_id), 10.5, 5.0, 4, (int)FilterTypes.BUTTERWORTH, 0.0);
                        Debug.Log("Filtered channel " + AlphaChannel2);
                        break;
                    case 3:
                        Channel3_Notchfiltered = DataFilter.remove_environmental_noise(unprocessed_data.GetRow(eeg_channels[i]), BoardShim.get_sampling_rate(board_id), (int)NoiseTypes.SIXTY);
                        Debug.Log("NotchFiltered channel " + eeg_channels[i]);
                        Debug.Log("[{3}]" + ", " + Channel3_Notchfiltered);
                        // alpha bandpass filter
                        double[] AlphaChannel3 = DataFilter.perform_bandpass(Channel3_Notchfiltered, BoardShim.get_sampling_rate(board_id), 10.5, 5.0, 4, (int)FilterTypes.BUTTERWORTH, 0.0);
                        Debug.Log("Filtered channel " + eeg_channels[i]);
                        // beta bandpass filter
                        break;
                }
            }
        }
        catch (BrainFlowException e)
        {
            Debug.Log(e);
        }
    }

    // Update is called once per frame
   
    // you need to call release_session and ensure that all resources correctly released
    private void OnDestroy()
    {
        if (board_shim != null)
        {
            try
            {
                board_shim.release_session();
            }
            catch (BrainFlowException e)
            {
                Debug.Log(e);
            }
            Debug.Log("Brainflow streaming was stopped");
        }
    }
}