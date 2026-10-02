$timestamp = (Get-Date).ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.ffffff")
$content = "on-session-start-hook-1 $timestamp"
Set-Content -Path "on-session-start-hook-1.txt" -Value $content -Encoding UTF8
