using MonoGameLibrary;
using MonoGameLibrary.Scenes;
using System;
using System.Drawing;
using System.Numerics;
using System.Reflection.Metadata;

namespace Shared.Scenes;

public class TitleScene : Scene
{
    private readonly ITitleUI titleUI;

    public bool IsMenuVisible { get; set; } = true;
    public bool IsLoading { get; set; }

    public event Action OnGameStartRequested;
    public event Action OnGameLoadRequested;
    public event Action OnSettingsRequested;
    public event Action OnExitRequested;

    public TitleScene(ITitleUI titleUI)
    {
        this.titleUI = titleUI;
    }

    public override void Initialize()
    {
        base.Initialize();
        SetupUI();
        SetupEventHandlers();
    }

    private void SetupEventHandlers()
    {
        titleUI.StartGameClicked += () => OnGameStartRequested?.Invoke();
        titleUI.LoadGameClicked += () => OnGameLoadRequested?.Invoke();
        titleUI.SettingsClicked += () => OnSettingsRequested?.Invoke();
        titleUI.ExitClicked += () => OnExitRequested?.Invoke();
    }

    public override void Update(float gameTime)
    {
        titleUI?.Update(gameTime);
        // Add title-specific logic (animations, background effects, etc.)
    }

    public override void Draw(float gameTime)
    {
        titleUI?.Draw(gameTime);
        // Add title-specific rendering
    }

    private void SetupUI()
    {
        titleUI.Initialize();
        IsMenuVisible = true;
    }
}


// Supporting enum for better state management
public enum TitleState
{
    MainMenu,
    Loading,
    Settings,
    Transitioning
}

// Optional: Interface for better abstraction
public interface ITitleUI
{
    event Action StartGameClicked;
    event Action LoadGameClicked;
    event Action SettingsClicked;
    event Action ExitClicked;

    void Initialize();
    void Update(float deltaTime);
    void Draw(float deltaTime);
    void ShowLoading(bool show);
}