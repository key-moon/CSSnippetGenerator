# 概要
[Library](https://github.com/key-moon/Library)からスニペットを生成するやつ

# 使い方
`dotnet run [ライブラリディレクトリへのパス]`<br>

`[指定したディレクトリ]/snippets/snippet.snippet`にスニペットファイルが生成されます。

# 環境
`dotnet core 3.0`が必要です。

# TODO
スニペットをRoslynとかでパースして文脈を把握できるようにしたい 

# 仕様
`[指定したディレクトリ]/src`下にある`*.csx`にマッチする全てのファイルを走査します。コードファイルとして解釈されたファイルを全てパースし、一つのスニペットファイルに纏めます。

# コードファイルの仕様
## Headerブロック(必須)
### 説明
Header要素を生成するブロックです。これがない場合、C#スクリプトファイルはコードファイルとして解釈されません。<br>
最初に`//`が含まれた行から含まれなくなる行の前までがブロックとして解釈されます。(よって、このブロックの要素の行は連続している必要があります。)<br>
行頭の連続する`/`は無視されます。<br>
Headerブロックの要素は`:`で区切られ、1要素目が要素名、2要素目が要素となります。
### 例
#### C#コード
``` cs
///Title: Power
///Shortcut: pow
///Description: 結合則が成り立つ乗算の冪演算
///Author: keymoon
```
#### 生成されるスニペット
``` xml
<Header>
  <Title>Power</Title>
  <Shortcut>pow</Shortcut>
  <Description>結合則が成り立つ乗算の冪演算</Description>
  <Author>keymoon</Author>
</Header>
```

## Usingブロック(任意)
### 説明
スニペットのImports要素を生成するブロックです。<br>
HeaderブロックからDeclarationsブロックまたはコードブロックの間に挿入された、`using`で開始する任意の行がこのブロックの要素であると解釈されます。
### 例
#### C#コード
``` cs
using System;
using Debug = System.Diagnostics.Debug;
using static System.Math;
```
#### 生成されるスニペット
``` xml
<Imports>
  <Import>
    <Namespace>System</Namespace>
  </Import>
  <Import>
    <Namespace>Debug = System.Diagnostics.Debug</Namespace>
  </Import>
  <Import>
    <Namespace>static System.Math</Namespace>
  </Import>
</Imports>
```

## Declarationsブロック(任意)
### 説明
スニペットのDeclarations要素を生成するブロックです。
`#if !DECLARATIONS`から対応する`#endif`までがブロックであると解釈されます。
#### Tooltip要素
ブロック内部で`//`から始まる行は説明要素として解釈されます。<br>
これは直後のリテラル要素のTooltipとなります。複数ある場合は結合されます。
#### リテラル要素
説明要素でない要素のうち、ブロック内部で`=`を含む行はリテラル要素として解釈されます。<br>
最後の`=`のみのトークンの前のトークンがID要素、後のトークンがDefault要素となります。空白やセミコロンは無視されます。<br>
ID要素の語頭に含まれる`@`はIDとしては無視されますが、コードブロックのパース時には無視されません。これにより、名前の衝突を回避できます。<br>
IDが重複した場合、最も上の行に記されたもの以外は無視されます。
### 例
#### C#コード
``` cs
#if !DECLARATIONS
/*
//型
@T = long
*/
using @T = System.Int64;
//単位元
static @T @Identity = 1;
#endif
```
#### 生成されるスニペット
``` xml
<Declarations>
  <Literal>
    <ID>T</ID>
    <ToolTip>型</ToolTip>
    <Default>long</Default>
  </Literal>
  <Literal>
    <ID>Identity</ID>
    <ToolTip>単位元</ToolTip>
    <Default>1</Default>
  </Literal>
</Declarations>
```

## コードブロック(必須)
コードそのものです。<br>
Headerブロックの後で、空行,Usingブロックの要素,Declarationsブロックのどれでもない行があった場合、そこから最後の行までがコードブロックとなります。ブロック先頭,末尾の空白文字は無視されます。<br>
リテラル要素に含まれるIDと一致した文字列は全て置換可能リテラルに置換されます。<br>
`/*cursor*/`という文字列があった場合、スニペット挿入後にそこにテキストカーソルが合うようになります。<br>
### 例
#### C#コード
``` cs
static @T Power(@T n, long m)
{
    @T pow = n;
    @T res = @Identity;
    while (m > 0)
    {
        if ((m & 1) == 1) res *= pow;
        pow *= pow;
        m >>= 1;
    }
    return res;/*cursor*/
}
```
*注:リテラル要素とカーソル要素は本来併用できないが、解説のために併用している。正常にパースはされるが、カーソル要素は機能しなくなる。*
#### 生成されるスニペット
``` xml
<Code Language="csharp">static $T$ Power($T$ n, long m)
{
    $T$ pow = n;
    $T$ res = $Identity$;
    while (m &gt; 0)
    {
        if ((m &amp; 1) == 1) res *= pow;
        pow *= pow;
        m &gt;&gt;= 1;
    }
    return res;
}</Code>
```
