# 贪吃蛇方块游戏面板

预制体：Assets/Resources/UI/Panel/GamePanel.prefab
预览图：Assets/Resources/ArtRes/Preview/Gameplay/snetris_panel_9x18.png

使用已有的天空背景、得分面板、棋盘面板、糖果方块和图标按钮拼装，没有修改现有脚本和场景。

## 尺寸与使用

- 设计尺寸 1080 × 1920，与当前 Canvas 的参考分辨率一致。
- 棋盘 9 列 × 18 行，共 162 格。每个格子的 RectTransform 为 60 × 60，格距 63。
- 拖入已有 Canvas，或通过 UIManager.Instance.ShowPanel<GamePanel>() 创建。
- 预制体挂载现有 GamePanel 脚本。该脚本目前未实现游戏逻辑；按钮点击事件留空，需后续绑定。
- 当前为竖屏布局。未增加横屏或其他比例的适配逻辑。

## 层级

- Background：天空背景。
- PortraitLayout/HUD：当前得分、连击、最高纪录；数字为演示值。
- PortraitLayout/Board/Grid_9Columns_18Rows：162 个独立 UI Image。
- 格子名字 Cell_R00_C00 表示最底行、最左列。行号 00–17，自下往上；列号 00–08，自左往右。
- PortraitLayout/Board/DemoPieces：演示蛇、食物与已落定方块。做实际游戏时先禁用或删除此组。
- PortraitLayout/Controls：BtnHome、BtnLeft、BtnDown、BtnRight、BtnPause，均含 Button 组件。
- 棋盘和状态栏使用原面板纹理的九块 RawImage 拼接，保留圆角，不修改原素材导入配置。

预览图根据相同素材与布局离线拼装，文字使用本机微软雅黑。Unity Text 沿用项目原有的内置字体引用，字体与导入后的最终效果以编辑器为准。已做静态结构与引用检查，未在 Unity 中运行验证。

## 棋盘尺寸依据

Snetris 官网介绍：蛇碰撞后落定成为彩色方块，同色 3×3 方阵消除。
https://www.snetris.com/

作者在开发分享中说明棋盘使用 9×18。此次采用这一尺寸，不将其视为所有蛇与方块混合游戏的统一标准。
https://www.reddit.com/r/IndieDev/comments/1sev11m/i_had_the_idea_to_combine_snake_and_tetris_8/
