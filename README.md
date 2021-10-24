# EditorAudioUtils

*Like [EditorApplication.Beep()](https://docs.unity3d.com/ScriptReference/EditorApplication.Beep.html), but with configurable sounds*

![EditorAudioUtils Settings Screenshot](Documentation~/preview.png)

[![openupm](https://img.shields.io/npm/v/com.jd.editoraudioutils?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.jd.editoraudioutils/)  [![Releases](https://img.shields.io/github/release-date/johannesdeml/editoraudioutils.svg)](https://github.com/johannesdeml/editoraudioutils/releases)  [![Unity 2019.1 or later](https://img.shields.io/badge/unity-2019.1%20or%20later-blue.svg?logo=unity&cacheSeconds=2592000)](https://unity3d.com/get-unity/download/archive)

## Installation
Install the package with [OpenUPM](https://openupm.com/)

```sh
$ openupm add com.jd.editoraudioutils
```

or download the [Latest Unity Packages](../../releases/latest)

## Features

* Set your own notification sounds through Project Settings -> EditorAudioUtils
* Play custom notification sounds through a simple API
* Play any AudioClip in the editor without the need of an AudioSource
* Disable notification sounds through EditorPrefs (therefore, each user can decide if they want the sounds or not)

## API

Play a predefined sound:
```csharp
EditorAudioUtility.PlayNotificationSound(EditorNotificationSound type);
```

Play any AudioClip in the editor:
```csharp
EditorAudioUtility.PlayAudioClip(AudioClip audioClip);
```


## License

* MIT - see [LICENSE](./LICENSE.md)
* Sounds in Sample are from [Kenneys Assets](https://kenney.nl/) (CC0)

