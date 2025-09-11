import whisper
from moviepy import VideoFileClip
import re

video_path = r"C:\Users\vinhl\OneDrive\Desktop\8bit\zeztz.mkv"
audio_path = r"C:\Users\vinhl\OneDrive\Desktop\8bit\audio_extracted.wav"
output_transcript_path = "transcript.txt"

def extract_audio(video_path, audio_output_path):
    print("Đang trích xuất âm thanh từ video...")
    video = VideoFileClip(video_path)
    video.audio.write_audiofile(audio_output_path, codec='pcm_s16le')
    print(f"Đã trích xuất âm thanh xong: {audio_output_path}")

def format_time_srt(seconds):
    hours = int(seconds // 3600)
    minutes = int((seconds % 3600) // 60)
    secs = int(seconds % 60)
    millis = int((seconds - int(seconds)) * 1000)
    return f"{hours:02d}:{minutes:02d}:{secs:02d},{millis:03d}"

def transcribe_and_export(audio_path, output_path, model_type="medium"):
    print("Đang nhận diện giọng nói (Speech-to-Text)...")
    
    model = whisper.load_model(model_type)
    result = model.transcribe(audio_path, language="ja")

    with open(output_path, "w", encoding="utf-8") as f:
        for i, segment in enumerate(result["segments"]):
            start = format_time_srt(segment["start"])
            end = format_time_srt(segment["end"])
            text = segment["text"].strip()
            
            f.write(f"[{start} --> {end}] {text}\n")
    
    print(f"Đã xuất transcript với timeline: {output_path}")
    
    srt_output_path = "subtitle_ja.srt"
    with open(srt_output_path, "w", encoding="utf-8") as srt_file:
        for i, segment in enumerate(result["segments"]):
            start = format_time_srt(segment["start"])
            end = format_time_srt(segment["end"])
            text = segment["text"].strip()
            
            srt_file.write(f"{i+1}\n")
            srt_file.write(f"{start} --> {end}\n")
            srt_file.write(f"{text}\n\n")

    print(f"Đã xuất phụ đề tiếng Nhật: {srt_output_path}")
    
    return result["text"]

if __name__ == "__main__":
    extract_audio(video_path, audio_path)
    transcript_text = transcribe_and_export(audio_path, output_transcript_path, model_type="medium")
    print("\nXem trước một đoạn transcript gốc:")
    print(transcript_text[:300] + "...")