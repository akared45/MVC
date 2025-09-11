from moviepy import VideoFileClip

# Đường dẫn đến file video của bạn
video_path = r"C:\Users\vinhl\OneDrive\Desktop\8bit\zeztz.mkv"
  # THAY ĐỔI ĐƯỜNG DẪN NÀY
# Đường dẫn để lưu file audio xuất ra
audio_output_path = "audio_extracted.wav"  # Có thể đổi thành .mp3 nếu muốn

# Load video
video = VideoFileClip(video_path)

# Trích xuất và lưu phần âm thanh
video.audio.write_audiofile(audio_output_path)

print(f"✅ Đã trích xuất thành công! File audio: {audio_output_path}")