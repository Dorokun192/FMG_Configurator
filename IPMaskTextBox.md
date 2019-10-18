# IPMaskedTextBox

This user control is essential for capturing IP address-style data from the user. I borrowed the userControl from <a target="_blank" rel="noopener noreferrer" href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated">here</a>. Hats off to the author, because of him, I had a better understanding of how a userControl works, and how it can be reused.

What I will post here are the changes that I've made on the code-behind. I tailored it so that it can be used easier or as I prefer to use it. You can now clone the code from the Github link above. Then we'll proceed on what changes I've made to it.

Once you got the actual code, open the sln file using Visual Studio 2019 and then once loaded, click on the .cs file in the solution explorer.

What we will change here is that we will only add two (2) public methods in inside the region public methods. This is the original code:
```cs
#region public methods
public byte[] GetByteArray()
{
    byte[] userInput = new byte[4];

    userInput[0] = Convert.ToByte(firstBox.Text);
    userInput[1] = Convert.ToByte(secondBox.Text);
    userInput[2] = Convert.ToByte(thirdBox.Text);
    userInput[3] = Convert.ToByte(fourthBox.Text);

    return userInput;
}
#endregion
```

Next, we will add the `LoadIPAddress()` method that accepts string as an input.

First, we will detect if the string isn't null as this will produce some errors in the future. If yes, we need to return and cancel the method immediately. If not, we proceed. Simple, right?

If the string has value, then we declare a string array, just to make things easier. The string has a format of something like "111.111.111.111", so we can use the dot (.) symbol as a delimiter and perform a string split then save it to the string array. Lastly, we will copy the values of the array to their respective positions. The whole code looks like this:
```cs
public void LoadIPAddress(string strtoip)
{
    if (strtoip == "")
        return;
    else
    {
        string[] ipsplit = new string[4];

        for (int i = 0; i < 4; i++)
            ipsplit[i] = strtoip.Split('.')[i];

        firstBox.Text = ipsplit[0];
        secondBox.Text = ipsplit[1];
        thirdBox.Text = ipsplit[2];
        fourthBox.Text = ipsplit[3];
    }
}
```

Now, how do we get the all values of the IP address? The original program uses `GetByteArray()` method, but this is a pain in the buttocks for me to use, so I created another public method. This time has a return value. I call it the `GetString()` method.

This method first declare a string container to return. Then it will check if each box has value, and if one of them has no value, it will return an empty string.
>Do note that this is not permanent for now, as I'm still thinking of ways to improve this. For example, what if the user left the last box to be blank, should it be zero or do I stick to this? Well, for now, my answer is the latter.

If all of them have value, then all string values will be concatenated with other in proper orientation and be returned to the caller. The whole code looks like this:
```cs
public string GetString()
{
    string iptostr;

    if (firstBox.Text == "" | secondBox.Text == "" | thirdBox.Text == "" | fourthBox.Text == "")
        iptostr = "";
    else
        iptostr = firstBox.Text + "." + secondBox.Text + "." + thirdBox.Text + "." + fourthBox.Text;

    return iptostr;
}
```

That's it! Now, you can copy the whole xaml file (not the .cs file only) to your main project and rebuild the program. You can also rename the userControl filename but I didn't bother with it as it is a hassle to do so.

This user control will look like this:

![](img/maskbox.gif)
