# COM3D2 Accessory Physics

Livelier physics for dangling accessories in COM3D2:

- **A BepInEx plugin** that lets the chains of **nipple**, **belly button (navel)**, **earring** and
  **front-hole** accessories swing further than the game's own motion, with adjustable strength and how the
  swing spreads along the chain.
- **An offline auto-rigger** (`tools/autorig`) that gives single-bone dangling accessories multi-joint
  rigs, so chains and pendants bend instead of swinging as one stiff piece.

Works on its own, and together with
[COM3D2 Double Nipple Accessory](https://github.com/nanashi-187/COM3D2_Double_Nipple_Accessory) and
[COM3D2 Double Front Hole Accessory](https://github.com/nanashi-187/COM3D2_Double_Front_Hole_Accessory):
the second accessory those plugins add swings too.

## Requirements

- COM3D2 (64-bit)
- BepInEx 5

## Installation

Copy `COM3D2.AccessoryPhysics.dll` into:

```text
COM3D2/BepInEx/plugins/
```

Restart the game after installing or updating the plugin.

Upgrading from Double Nipple 0.7.x or Double Front Hole 0.4.0: their swing settings moved here. Update
those plugins too (Double Nipple 0.8.0, Double Front Hole 0.5.0 or later) so a slot is never swung twice,
and copy your old Swing Strength / Taper values into the matching section below.

## Swing settings

Settings are in `BepInEx/config/local.com3d2.accessoryphysics.cfg`, or in-game with ConfigurationManager
(F1); changes apply immediately. There is one section each for **Nipple**, **Belly Button**, **Earrings**
and **Front Hole** (both front-hole slots):

| Setting | Default | Effect |
| --- | --- | --- |
| `Enabled` | true | Turns the swing options on or off for that group. |
| `Swing Strength` | 1.5 | How far chains swing away from their settled pose, 0.5–4. 1 = the game's own motion. |
| `Swing Taper` | 0.5 | Spreads the extra swing along a chain, 0–3: 0 = equal at every joint; higher values keep the top stiffer and move the swing to the lowest joints, like a chain carrying a pendant. The total stays the same. |

How it works: after the game simulates and poses an accessory's chain each frame, each chain bone's
rotation away from its smoothed settled pose is scaled. The game's simulation itself is not changed. The
extra swing is capped at 120° per joint and reduced wherever it would push the chain deeper into the
body-collision spheres than the game's own pose.

Only accessories with chain physics are affected: those whose model has bones named `_yure_soft_`,
`_yure_hard_` or `_yure_`. Rigid accessories stay rigid, and accessories using dynamic-bone (`.phy`) physics
are left alone. Single-bone dangles swing as one piece, so Swing Taper has no effect on them; the
auto-rigger can split them into joints.

## Auto-rigger

See [tools/autorig/README.md](tools/autorig/README.md). In short: scan your Mod folder, then write rigged
copies, each with its own menus and a labelled icon (orange **RIG** = name looks like a chain or tassel,
purple **RIG?** = check it in-game), into a separate folder. Your mod files are never modified, and items
can be fixed or skipped per model with an overrides file.

## Building

From PowerShell:

```powershell
.\build.ps1 -GamePath 'C:\Games\COM3D2'
```

The DLL is written to `bin/COM3D2.AccessoryPhysics.dll`. Visual Studio 2022 Build Tools and its Roslyn
C# compiler are required.

## Notes

- The plugin does not modify game or mod asset files.
- The swing options are experimental. Report problems with the accessory's name and the BepInEx log.

## License

Nanashi-187 Non-Commercial No-Derivatives Software License 1.0. See `LICENSE` for the complete terms.

---

## 日本語

`COM3D2 Accessory Physics` は、COM3D2 のぶら下がるアクセサリーの揺れを強化するためのツールです。

- **BepInEx プラグイン**：**乳首**・**へそ**・**イヤリング**・**前穴**アクセサリーのチェーンを、ゲーム標準より大きく揺らします。
- **オフライン自動リガー**（`tools/autorig`）：揺れボーンが 1 本だけのアクセサリーに複数の関節を追加し、チェーンやペンダントが曲がるようにします。

単体で動作し、[Double Nipple Accessory](https://github.com/nanashi-187/COM3D2_Double_Nipple_Accessory) や [Double Front Hole Accessory](https://github.com/nanashi-187/COM3D2_Double_Front_Hole_Accessory) で追加した 2 つ目のアクセサリーも揺れます。

### インストール

`COM3D2.AccessoryPhysics.dll` を `COM3D2/BepInEx/plugins/` へコピーし、ゲームを再起動してください。

Double Nipple 0.7.x または Double Front Hole 0.4.0 から移行する場合：揺れ設定はこのプラグインへ移動しました。同じスロットが二重に揺れないよう、Double Nipple 0.8.0 / Double Front Hole 0.5.0 以降に更新し、以前の Swing Strength / Taper の値を下記の該当セクションへ設定してください。

### 揺れ設定

`BepInEx/config/local.com3d2.accessoryphysics.cfg`、またはゲーム内の ConfigurationManager（F1）で変更できます。`Nipple`（乳首）・`Belly Button`（へそ）・`Earrings`（イヤリング）・`Front Hole`（前穴、両スロット）それぞれに次の設定があります。

- `Enabled`：揺れ設定の有効／無効（既定 true）
- `Swing Strength`：揺れの強さ、0.5〜4（既定 1.5、1 = ゲーム標準）
- `Swing Taper`：揺れの配分、0〜3（既定 0.5）。大きいほどチェーン上部は硬く、下端ほど大きく揺れます。

ゲームの物理計算自体は変更しません。体の当たり判定より深く食い込む揺れは抑えられます。揺れボーン（`_yure_soft_` / `_yure_hard_` / `_yure_`）を持たないアクセサリーは揺れません。

### 自動リガー

使い方は [tools/autorig/README.md](tools/autorig/README.md) を参照してください（英語）。MOD ファイルは変更せず、別フォルダーに複製を作成します。

### ライセンス

Nanashi-187 Non-Commercial No-Derivatives Software License 1.0。詳細は `LICENSE` を参照してください。

## License Update 1.1 / ライセンス更新 1.1

The current license is now **Nanashi-187 Non-Commercial No-Derivatives Software License 1.1**, which replaces references to version 1.0 elsewhere in this README. Version 1.1 adds restrictions on scraping, text and data mining, and AI or machine learning training. See `LICENSE` for the complete terms.

現在のライセンスは **Nanashi-187 Non-Commercial No-Derivatives Software License 1.1** です。本 README 内のバージョン 1.0 への言及は 1.1 に置き換えられます。バージョン 1.1 では、スクレイピング、テキスト・データマイニング、AI・機械学習の学習への利用に関する制限を追加しました。完全な条件は `LICENSE` を参照してください。
