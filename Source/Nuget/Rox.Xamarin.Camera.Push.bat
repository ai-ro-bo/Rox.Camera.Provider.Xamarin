echo off
cls
echo Start pushing...
nuget push "bin\Rox.Xamarin.Camera.1.2.0-pre1.nupkg" {GIT_TOKEN} -s "https://api.nuget.org/v3/index.json"
echo Done pushing.
pause
echo on
