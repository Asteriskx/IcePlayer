using System;
using System.IO;

using CoreTweet;

namespace IcePlayer.Models
{
    /// <summary>
    /// NowPlaying : Twitter に 現在再生中の曲を投稿するクラス
    /// </summary>
    public class NowPlaying
    {
        #region 認証パラメータ
        public static readonly string Consumer_Key    　　= "Your_Consumer_Key";
        public static readonly string Consumer_Secret     = "Your_Consumer_Secret";
        public static readonly string Access_Token        = "Your_Access_Token";
        public static readonly string Access_Token_Secret = "Your_Access_Token_Secret";
        #endregion

        /// <summary>
        /// Random クラスインスタンス。 
        /// </summary>
        private Random rdm;

        /// <summary>
        /// 乱数シード生成変数。
        /// </summary>
        private int seed;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NowPlaying()
        {
            seed = Environment.TickCount;
            rdm  = new Random( seed++ );
        }

        /// <summary>
        /// 現在再生している曲を Twitter に投稿します。
        /// <param name=Track>現在再生している曲名。</param>
        /// <param name=Artist>現在再生している曲のアーティスト名。</param>
        /// </summary>
        public bool Tweet( string Track, string Album, string Artist, int PlayedCount )
        {
            // API にアクセスするためのトークン群
            var tokens = Tokens.Create(
                $"{Consumer_Key}",
                $"{Consumer_Secret}",
                $"{Access_Token}",
                $"{Access_Token_Secret}"
            );

            // Tweet 用に文字列を生成する
            string Mix = "#NowPlaying\n" + $"{Track}" + " \nby" + $" {Artist}\n" + "#IcePlayer";

            try
            {
                // 再生中に生成されたジャケット画像をここで参照する。
                MediaUploadResult res = tokens.Media.Upload(media =>
                    new FileInfo(
                        @"※生成されたアートワークを指定する事。(artworks.jpg)"
                    )
                );

                tokens.Statuses.Update( 
                    new {
                        media_ids = new long[]{ res.MediaId },
                        status    = Mix
                    } 
                );

                Console.WriteLine($"{Mix}");
            }

            // ツイートが重複した際に catch する。
            catch ( Exception ex )
            {
                string msg = $"IcePlayer is Tweet Overlapped. error_code : {rdm.Next(10, 100)}";

                Console.WriteLine( ex.Message );
                tokens.Statuses.Update( new { status = msg } );

                return false;
            }

            return true;
        }
    }
}