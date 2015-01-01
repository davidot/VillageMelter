using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VillageMelter.Base
{
    public class InputHandler
    {
        private VillageMelter game;

        private KeyboardState oldKeyState;

        private KeyboardState newKeyState;

        private MouseState oldMouseState;

        private MouseState newMouseState;

        private int oldScrollWheel = 0;

        private int newScrollWheel = 0;

        public InputHandler(VillageMelter game)
        {
            this.game = game;
            //Setting initial state to avoid crashes by accesesing input before game has started
            oldMouseState = Mouse.GetState();
            oldKeyState = Keyboard.GetState();
            oldScrollWheel = oldMouseState.ScrollWheelValue;

            newMouseState = Mouse.GetState();
            newKeyState = Keyboard.GetState();
            newScrollWheel = newMouseState.ScrollWheelValue;
        }

        public void Update()
        {
            Update(Keyboard.GetState(), Mouse.GetState());
        }

        public void Update(KeyboardState keyboardState, MouseState mouseState)
        {
            oldKeyState = newKeyState;
            oldMouseState = newMouseState;
            oldScrollWheel = newScrollWheel;
            newKeyState = keyboardState;
            newMouseState = mouseState;
            newScrollWheel = newMouseState.ScrollWheelValue;
        }


        public bool IsKeyPressed(Keys key)
        {
            return oldKeyState.IsKeyUp(key) && newKeyState.IsKeyDown(key);
        }

        public bool IsKeyDown(Keys key)
        {
            return newKeyState.IsKeyDown(key);
        }

        public bool IsLeftMousePressed()
        {
            return oldMouseState.LeftButton != newMouseState.LeftButton && newMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool IsRightMousePressed()
        {
            return oldMouseState.RightButton != newMouseState.RightButton && newMouseState.RightButton == ButtonState.Pressed;
        }

        public bool IsMiddleMousePressed()
        {
            return oldMouseState.MiddleButton != newMouseState.MiddleButton && newMouseState.MiddleButton == ButtonState.Pressed;
        }

        public bool IsLeftMouseDown()
        {
            return newMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool IsRightButtonDown()
        {
            return newMouseState.RightButton == ButtonState.Pressed;
        }

        public bool IsMiddleMouseDown()
        {
            return newMouseState.MiddleButton == ButtonState.Pressed;
        }

        public Point MousePosition()
        {
            return newMouseState.Position;
        }

        public int ScrollWheelDifference()
        {
            return newScrollWheel - oldScrollWheel;
        }


    }
}
