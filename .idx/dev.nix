# To learn more about how to use Nix to configure your environment
# see: https://firebase.google.com/docs/studio/customize-workspace
{ pkgs, ... }: {
  # Which nixpkgs channel to use.
  channel = "unstable";

  # Use https://search.nixos.org/packages to find packages
  packages = [
    pkgs.dotnet-sdk_10
    pkgs.sqlite
  ];

  idx = {
    extensions = [
      "ms-dotnettools.csharp"
      "ms-dotnettools.csdevkit"
    ];
  };
}
