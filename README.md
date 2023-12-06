# Asteroids Game

## Overview

This project is an implementation of the classic Asteroids game using Unity.

## Game Overview

The Asteroids game is a timeless arcade classic where players control a spaceship, navigating through space, avoiding asteroids, and shooting them down. The goal is to survive as long as possible while earning points by destroying asteroids.

## Architecture and Design Patterns

### Zenject Dependency Injection

The project utilizes the Zenject framework for dependency injection. Zenject simplifies the management of dependencies and promotes a modular, decoupled architecture.

### Object Pooling

Object pooling is employed for frequently created and destroyed objects such as bullets, meteors, and the player ship. This optimizes memory usage and performance.