﻿using System;
using System.Threading.Tasks;

namespace IcePlayer.Models
{
    /// <summary>
    /// メディア プレーヤーの基本的なインターフェイスを提供します。
    /// </summary>
    public interface iInterFace : IDisposable
    {
        /// <summary>
        /// 非同期操作として現在音楽プレーヤーで再生している曲を取得します。
        /// </summary>
        /// <returns>
        /// <para>非同期操作を表すタスク オブジェクト。</para>
        /// <para>タスク オブジェクトの <c>Result</c> プロパティは、<see cref="IcePlayer.Models.IceMediaItem"/> を返します。</para>
        /// </returns>
        Task<IceMediaItem> GetCurrentMedia();

        /// <summary>
        /// 音楽プレーヤーの現在の再生状態を取得します。
        /// </summary>
        IceState PlayerState { get; }

        /// <summary>
        /// 音楽プレーヤーの現在の再生位置を取得または設定します。
        /// </summary>
        double CurrentPosition();

        /// <summary>
        /// 音楽プレーヤーの現在の再生音量を設定します。
        /// </summary>
        void CurrentVolume( double arg );

        /// <summary>
        /// 音楽プレーヤーで再生を実行します。
        /// </summary>
        void Play();

        /// <summary>
        /// 音楽プレーヤーで早送りを実行します。
        /// </summary>
        void FastForward();

        /// <summary>
        /// 音楽プレーヤーで巻戻しを実行します。
        /// </summary>
        void Rewind();

        /// <summary>
        /// 音楽プレーヤーで停止を実行します。
        /// </summary>
        void Stop();

        /// <summary>
        /// 音楽プレーヤーで一時停止を実行します。
        /// </summary>
        void Pause();

        /// <summary>
        /// 音楽プレーヤーで次の曲の再生を実行します。
        /// </summary>
        void NextTrack();

        /// <summary>
        /// 音楽プレーヤーで前の曲の再生を実行します。
        /// </summary>
        void PreviousTrack();
    }
}