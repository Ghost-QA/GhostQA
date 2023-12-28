﻿using ScreenRecorderLib;
using System;
using System.IO;
using System.Text;

namespace MyersAndStaufferFramework
{
    public class VideoRecorder
    {
        public static string basePath = @"D:\Mechlin Tech\MyersAndStauffer_GhostQA\SeleniumTestReport\";
        public static string videoPath = "";
        private static Recorder _rec;
        public static void CreateRecording()
        {
            _rec = Recorder.CreateRecorder();
            _rec.OnRecordingComplete += Rec_OnRecordingComplete;
            _rec.OnRecordingFailed += Rec_OnRecordingFailed;
            _rec.OnStatusChanged += Rec_OnStatusChanged;

            GenerateRecodingFileandStartrecoding();
        }

        static string GetRecordingFolderPath(string folderName)
        {
            videoPath = basePath;
            // Create a "Recording" subfolder
            string recordingFolderPath = Path.Combine(videoPath, folderName);

            if (!Directory.Exists(recordingFolderPath))
            {
                Directory.CreateDirectory(recordingFolderPath);
            }

            // Get the directory of the assembly
            return recordingFolderPath;
        }
        public static void EndRecording()
        {
            _rec.Stop();
        }

        private static void Rec_OnRecordingComplete(object sender, RecordingCompleteEventArgs e)
        {
            //Get the file path if recorded to a file
            string path = e.FilePath;
        }

        private static void Rec_OnRecordingFailed(object sender, RecordingFailedEventArgs e)
        {
            string error = e.Error;
        }

        private static void Rec_OnStatusChanged(object sender, RecordingStatusEventArgs e)
        {
            RecorderStatus status = e.Status;
        }

        static void GenerateRecodingFileandStartrecoding()
        {
            // Get the project folder path
            string projectFolderPath = GetRecordingFolderPath("Recordings");

            //Video Save Directory
            string videodir = Path.Combine(projectFolderPath, $"{DateTime.Now:yyyy-MM-dd}");
            if (!Directory.Exists(videodir))
            {
                Directory.CreateDirectory(videodir);
            }

            //Record to a file and save on local
            videoPath = Path.Combine(videodir, $"{DateTime.Now:yyyy-MM-dd_hh-mm-ss}.webm");

            //For saving on blob, we need to send this URL to DBContext and save this
            _rec.Record(videoPath);

            // Delete Extra Folder Generated by Library
            if (Directory.Exists(videoPath.Replace(".webm", "")))
            {
                Directory.Delete(videoPath.Replace(".webm", ""));
            }
        }
    }
}
