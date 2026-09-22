<#
.SYNOPSIS
    Launches a Windows Forms executable, waits for its main window, saves a
    screenshot of that window as a PNG, and terminates the process.
#>
param(
    [Parameter(Mandatory = $true)][string]$ExePath,
    [Parameter(Mandatory = $true)][string]$OutFile,
    [int]$TimeoutSeconds = 20
)

$ErrorActionPreference = "Stop"

Add-Type -AssemblyName System.Drawing

Add-Type @"
using System;
using System.Runtime.InteropServices;

public class NativeWin
{
    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}
"@

if (-not (Test-Path $ExePath)) {
    throw "Executable not found: $ExePath"
}

$proc = Start-Process -FilePath $ExePath -PassThru

try {
    $deadline = (Get-Date).AddSeconds($TimeoutSeconds)
    while ((Get-Date) -lt $deadline -and $proc.MainWindowHandle -eq [IntPtr]::Zero) {
        Start-Sleep -Milliseconds 250
        $proc.Refresh()
    }

    if ($proc.MainWindowHandle -eq [IntPtr]::Zero) {
        throw "Timed out waiting for '$ExePath' to show its main window."
    }

    # Give the window a moment to finish rendering.
    Start-Sleep -Seconds 1
    [NativeWin]::SetForegroundWindow($proc.MainWindowHandle) | Out-Null
    Start-Sleep -Milliseconds 500

    $rect = New-Object NativeWin+RECT
    if (-not [NativeWin]::GetWindowRect($proc.MainWindowHandle, [ref]$rect)) {
        throw "Failed to read the window bounds for '$ExePath'."
    }

    $width = $rect.Right - $rect.Left
    $height = $rect.Bottom - $rect.Top

    $bitmap = New-Object System.Drawing.Bitmap $width, $height
    $graphics = [System.Drawing.Graphics]::FromImage($bitmap)
    try {
        $graphics.CopyFromScreen($rect.Left, $rect.Top, 0, 0, $bitmap.Size)
        $bitmap.Save($OutFile, [System.Drawing.Imaging.ImageFormat]::Png)
    }
    finally {
        $graphics.Dispose()
        $bitmap.Dispose()
    }
}
finally {
    if (-not $proc.HasExited) {
        Stop-Process -Id $proc.Id -Force
    }
}
