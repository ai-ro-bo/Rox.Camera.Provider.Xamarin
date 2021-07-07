echo off
cls
echo Start packing...
nuget pack "Rox.Xamarin.Camera.nuspec" -OutputDirectory "bin"
echo Done packing.
pause
echo on
