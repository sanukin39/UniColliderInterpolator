# UniColliderInterpolator

![Image](https://github.com/sanukin39/UniColliderInterpolator/blob/master/Demo/interpolated01.png)

## Overview
UniColliderInterpolater is the plugin of Unity that interpolate 3d model to cube collider.
The Unity physics does not allowed non convex(concave) mesh collider. We can escape the problem by checking convex option at mesh collider but the　collision detection is not acculate. The plugin aims accurate collision detection by interpolate mesh collider using box collider.

## Demo
Left : Mesh collider with default convex option.
Right : Interpolated mesh collider using UniColliderInterpolator.
![Demo1](https://github.com/sanukin39/UniColliderInterpolator/blob/master/Demo/useconvex.gif)
![Demo2](https://github.com/sanukin39/UniColliderInterpolator/blob/master/Demo/interpolated.gif)


## Requirement
Unity 2018 or higher

## Usage
Attach ColliderInterpolator.cs to your 3d model whitch has mesh filter.

![Attach](https://github.com/sanukin39/UniColliderInterpolator/blob/master/Demo/attach.png)

Set collider division length and push Generate button.

![Attach](https://github.com/sanukin39/UniColliderInterpolator/blob/master/Demo/execute.png)

That's it!!

![Demo3](https://github.com/sanukin39/UniColliderInterpolator/blob/master/Demo/generation.gif)

## Notice
- The plugin detect other object collider when generation. Separete target 3d model from other colliders when generation.
- The generation doesnt work well when target 3d model has rigidbody. Remove rigidbody components from target model when generation.

## Install
use unitypackage at [release](https://github.com/sanukin39/UniColliderInterpolator/releases/) page

## Licence
[MIT](https://github.com/sanukin39/UniColliderInterpolator/blob/master/LICENSE)

## Author
[sanukin39](https://github.com/sanukin39)

## Bench
The great bench model from [Unity Asset Store](https://assetstore.unity.com/packages/3d/props/exterior/street-bench-656).
