<p align="center">
  <a href="https://github.com/mk-gg/hp-bars">
    <img src="https://github.com/mk-gg/hp-bars/blob/main/preview/preview.png" alt="Master" style="width:50%; height:auto;">
  </a>
</p>


<p align="center">
  <h1>Stardew Valley Mod - HP Bars</h1>
</p>

A Stardew Valley mod that adds health bars above monsters. 

## Installation

1. Install [SMAPI](https://smapi.io/)
2. Download the latest version of HP Bars
3. Extract the zip file into your Stardew Valley Mods folder
4. Run the game using SMAPI

## Configuration

The mod's settings can be configured in the `config.json` file:

```json
{
  "Show_Full_Life": true,    
  "Range_Verification": false
}
```
`Show_Full_Life`  Show health bars for monsters at full health

`Range_Verification` Only show health bars for monsters within range

## Console Commands

The mod provides the following in-game commands:

- `hpbars_showfull <true/false>` - Toggle health bars for full-health monsters
- `hpbars_range <true/false>` - Toggle range verification for health bars


## Known Issues

- This mod may not support monsters added by other mods.

## Resources
All credit goes to:
- [SMAPI Docs](https://stardewvalleywiki.com/Modding:Modder_Guide/APIs)
- [Enemy Health Bar by OrSpeeder](https://www.nexusmods.com/stardewvalley/mods/193)
- [Mini Bars by Coldopa](https://www.nexusmods.com/stardewvalley/mods/8329)

## License

This repository's source code is available under the [MIT License](LICENSE).