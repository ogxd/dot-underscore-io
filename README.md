# dot-underscore-io

I reverse engineered the "._xxx" files in which MacOS stores associated file metadata, using a custom hex tool I've made, 010 Editor and with the help of some online articles (see references).  
My initial plan is to combile this library with [tags-for-windows](https://github.com/ogxd/tags-for-windows).

## Todo
- [x] Make the read work
- [x] Make the write work
- [x] Add a sample with tags
- [ ] Cleanup code
- [ ] Add more samples

## References
* [Understanding Apple’s binary property list format by Christos Karaiskos](https://medium.com/@karaiskc/understanding-apples-binary-property-list-format-281e6da00dbd)
* [The ._ (dot-underscore) file format by Yogesh Khatri](https://www.swiftforensics.com/2018/11/the-dot-underscore-file-format.html)
